using cila.Domain;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using cila.Domain.Infrastructure.Chains;
using cila.Domain.Infrastructure.MessageQueue;
using cila.Domain.Serializers;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using MongoDB.Bson;

namespace cila.Aggregator
{
    public class AggregatorService
    {
        public string Id { get; private set; }
        private readonly CilaSettings config;
        private EventsDispatcher _dispatcher;
        private readonly ChainClientsFactory chainClientsFactory;
        private KafkaProducer _producer;
        private readonly ChainsService chainsService;
        private readonly AggregagtedEventsService aggregagtedEventsService;

        public AggregatorService(CilaSettings config, EventsDispatcher dispatcher,
        ChainClientsFactory chainClientsFactory, KafkaProducer producer, ChainsService chainsService,
        AggregagtedEventsService aggregagtedEventsService)
        {
            this.config = config;
            _dispatcher = dispatcher;
            this.chainClientsFactory = chainClientsFactory;
            _producer = producer;
            this.chainsService = chainsService;
            this.aggregagtedEventsService = aggregagtedEventsService;
            Id = config.ExecutionEnvironmentId;
        }

        public async Task Aggregate()
        {
            var chains = chainsService.GetAll();
            //fetch the latest state for each chains
            Console.WriteLine("Current active chains: {0}", chains.Count);

            await Parallel.ForEachAsync(chains, async (chain, cancellationToken) =>
            {
                //var current = chain.LastSyncedBlock;
                var current = aggregagtedEventsService.GetLastVersion(config.AggregateID);
                var next = current != null ? current.Value + 1 : 0;
                var client = chainClientsFactory.GetChainClient(chain.ChainId);
                // Should be replace by pulling from block number
                var newEvents = await client.PullAsync(config.AggregateID, (uint)next);
                var aggregatedEvents = newEvents.Select(x =>
                {
                    var domainEvent = CilaDomainSerializer.DeserializeDomainEvent(x);
                    return new AggregatedEvent
                    {
                        DomainEvent = domainEvent,
                        Payload = x,
                        AggregateId = config.AggregateID, // replace with real aggregate ID
                        ChainId = chain.Id,
                        OperaionId = config.AggregateID + domainEvent.EvntIdx,
                        CommandId = config.AggregateID + domainEvent.EvntIdx,
                        BlockNumber = domainEvent.EvntIdx, //replace with block number,
                        BlockHash = null // should be repalced with real one
                    };
                });
                foreach (var e in aggregatedEvents)
                {
                    //Find if the event with this aggregagte ID and this number has been already preocessed by aggregagtor, 
                    // maybe include hash to check if there was a conflicing event, so if there is a conflicting event, than 
                    // we need to decide if we replace it with new one or not, it should be somehow provided in event metadata from the chain
                    // so we know that this event has been authorized by relay as an actual one, maybe even by relay timestamp

                    var existingEvents = aggregagtedEventsService.GetEvents(e.AggregateId, e.Version, e.Hash);
                    var conflict = existingEvents.Any();
                    if (!conflict)
                    {
                        _dispatcher.DispatchEvent(e.DomainEvent);
                    }
                    if (!existingEvents.Any(x => x.ChainId == chain.Id))
                    {
                        aggregagtedEventsService.AddEvent(e.ToMongoDocument());
                        var infEvent = new InfrastructureEvent
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            EvntType = InfrastructureEventType.EventsAggregatedEvent,
                            AggregatorId = this.Id,
                            OperationId = e.OperaionId ?? (e.AggregateId + e.Version),
                            ChainId = chain.Id,
                            Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
                        };
                        infEvent.Events.Add(new DomainEventDto
                        {
                            Id = e.Hash,
                            Timespan = Timestamp.FromDateTime(DateTime.UtcNow),
                            AggregateId = e.AggregateId,
                            CommandId = e.CommandId,
                            SourceId = chain.Id,
                            Conflict = conflict
                        });
                        await _producer.ProduceAsync("infr", infEvent);
                    }
                }
            });
            // find new events and dispatch them to events dispatcher
        }
    }
}