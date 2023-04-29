using cila.Domain;
using cila.Domain.Database;
using cila.Domain.Database.Services;
using cila.Domain.Infrastructure.Chains;
using cila.Domain.Infrastructure.MessageQueue;
using cila.Relay;

namespace cila.Relay
{

    public class RelayService
    {
        private List<IExecutionChain> _chains;

        public string Id { get; private set; }

        public RelayService(CilaSettings config)
        {
            _chains = new List<IExecutionChain>();
            Id = config.ExecutionEnvironmentId;
            var random = new Random();
            var db = new MongoDatabase(config);
            var subsService = new SubscriptionsService(db);
            var kafkraProducer = new KafkaProducer(Program.ProducerConfig());
            var subs = subsService.GetAllFor(config.AggregateID).ToList();
            foreach (var chain in config.Chains)
            {
                if (subs.Count(x => x.ChainId == chain.ChainId) == 0)
                {
                    subsService.Create(config.AggregateID, chain.ChainId);
                }
                var execChain = new ExecutionChain(config.AggregateID, new EventStore(db), new EventsDispatcher(subsService, config, kafkraProducer), kafkraProducer);
                execChain.ID = chain.ChainId;
                execChain.ChainService = new EthChainClient(chain.Rpc, chain.DispatcherContract, chain.EventStoreContract, chain.PrivateKey);
                var relay = execChain.ChainService.GetRelayPermission().GetAwaiter().GetResult();
                Console.WriteLine("Creating chain with RPC: {0}, Private Key: {2}, Contract: {1}, Relay: {3}", chain.Rpc, chain.EventStoreContract, chain.PrivateKey, relay);
                _chains.Add(execChain);
            }
        }

        public void SyncAllChains()
        {
            //fetch the latest state for each chains
            Console.WriteLine("Active chains: {0}", _chains.Count);
            foreach (var chain in _chains)
            {
                chain.Update(Id);
            }
        }
    }
}