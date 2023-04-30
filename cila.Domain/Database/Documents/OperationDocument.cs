using Amazon.Runtime.Internal.Transform;

namespace cila.Domain.Database.Documents
{
    public class OperationDocument
    {
        public string Id {get;set;}
        public string ClientID {get;set;}
        public string AggregateId {get;set;}
        public string RouterId {get;set;}
        public DateTime Created {get;set;}
        public List<string> Commands {get;set;}
        public List<string> Events {get;set;}
        public List<SyncItems> Routers {get;set;}
        public List<SyncItems> Chains {get;set;}
        public List<SyncItems> Relays {get;set;}
        public List<SyncItems> Aggregators {get;set;}
        public List<InfrastructureEventItem> InfrastructureEvents {get;set;}
        public List<OperationChainStatusItem> PerChainStatus { get; set; }

        public OperationDocument()
        {
            InfrastructureEvents  = new List<InfrastructureEventItem>();
            Commands = new List<string>();
            Relays = new List<SyncItems>();
            Chains = new List<SyncItems>();
            Aggregators = new List<SyncItems>();
            Routers = new List<SyncItems>();
            Events = new List<string>();
            PerChainStatus = new List<OperationChainStatusItem>()
            {
                new OperationChainStatusItem("5"),
                new OperationChainStatusItem("11155111"),
                new OperationChainStatusItem("1313161555")
            };
        }
    }

    public class SyncItems
    {
        public string Id {get;set;}
        public string Name {get;set;}
        public bool OriginalSource {get;set;}
        public DateTime Timestamp {get;set;}
        public string ErrorMessage {get;set;}
    }

    public class InfrastructureEventItem {
        public DateTime Timestamp {get;set;}

        public InfrastructureEventType Type {get;set;}

        public string PortalId { get; set; }

        public string EventId {get;set;}

        public string RouterId {get;set;}

        public string RelayId {get;set;}

        public string CoreId {get;set;}

        public string ChainId { get; set; }

        public string AggreggatorId {get;set;}

        public string OperationId {get;set;}

        public List<string> DomainEvents {get;set;}

        public List<string> DomainCommands {get;set;}
    }

    public class OperationChainStatusItem
    {
        private  readonly Dictionary<string, string> _chainNames = new Dictionary<string, string>
        {
            { "5", "Ethereum Goerli" },
            { "11155111", "Ethereum Sepolia" },
            { "1313161555", "Aurora Testnet" }
        };

        public string ChainId { get; private set; }
        public string ChainName { get; private set; }
        public ChainStatus Status { get; set; }

        public OperationChainStatusItem(string chainId)
        {
            Status = ChainStatus.NotSynced;
            ChainId = chainId;
            ChainName = _chainNames[chainId];
        }
    }

    public enum ChainStatus
    {
        Synced,
        InSync,
        NotSynced
    }

}