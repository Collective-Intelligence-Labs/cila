using System.Linq.Expressions;
using cila.Domain;
using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cila.Aggregator
{
    public class InfrastructureEventsHandler : IEventHandler
    {
        private readonly OperationsService _operationsService;

        public InfrastructureEventsHandler(OperationsService operationsService)
        {
            _operationsService = operationsService;
        }

        public void Handle(InfrastructureEvent e)
        {

            var doc = _operationsService.FindOne(e.OperationId);
            if (doc == null)
            {
                doc = _operationsService.Create(
                    e.OperationId,
                    e.Commands.Select(x => x.Id).ToList(),
                    e.PortalId,
                    e.Timestamp.ToDateTime());
            }

            var infEv = CreateNewInfrastructureEvent(e);

            var syncItem = new SyncItems
            {
                Timestamp = e.Timestamp.ToDateTime(),
                OriginalSource = !e.Events.Any(x => x.Conflict),
                ErrorMessage = e.ErrorMessage
            };

            switch (e.EvntType)
            {
                case InfrastructureEventType.TransactionRoutedEvent:
                    syncItem.Id = e.RouterId;
                    syncItem.Name = "Router " + syncItem.Id;
                    _operationsService.InsertNewSyncItem(doc, x => x.Routers, syncItem);
                    break;
                case InfrastructureEventType.EventsAggregatedEvent:
                    syncItem.Id = e.AggregatorId;
                    syncItem.Name = "Aggregator " + syncItem.Id;
                    _operationsService.InsertNewSyncItem(doc, x => x.Aggregators, syncItem);
                    break;
                case InfrastructureEventType.TransactionExecutedEvent:
                    syncItem.Id = e.ChainId;
                    syncItem.Name = "Chain " + syncItem.Id;
                    _operationsService.InsertNewSyncItem(doc, x => x.Chains, syncItem);
                    _operationsService.UpdateChainStatus(doc, e.ChainId, ChainStatus.Synced);
                    break;
                case InfrastructureEventType.RelayEventsTransmiitedEvent:
                    syncItem.Id = e.RelayId;
                    syncItem.Name = "Relay " + syncItem.Id;
                    _operationsService.InsertNewSyncItem(doc, x => x.Relays, syncItem);
                    _operationsService.UpdateChainStatus(doc, e.ChainId, ChainStatus.InSync);
                    break;
                case InfrastructureEventType.ApplicationOperationInitiatedEvent:
                    break;
                default:
                    break;
            }

            _operationsService.InsertNewEvent(e.OperationId, infEv);

        }

        private InfrastructureEventItem CreateNewInfrastructureEvent(InfrastructureEvent e)
        {
            return new InfrastructureEventItem
            {
                PortalId = e.PortalId,
                OperationId = e.OperationId,
                AggreggatorId = e.AggregatorId,
                RouterId = e.RouterId,
                RelayId = e.RelayId,
                EventId = e.Id,
                ChainId = e.ChainId,
                CoreId = e.CoreId,
                DomainEvents = e.Events.Select(x => x.ToJson()).ToList(),
                DomainCommands = e.Commands.Select(x => x.ToJson()).ToList(),
                Type = e.EvntType,
                Timestamp = e.Timestamp.ToDateTime()
            };
        }
    }
}