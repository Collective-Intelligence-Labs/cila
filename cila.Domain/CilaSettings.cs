namespace cila.Domain
{
    public class CilaSettings
    {
        public string MongoDBConnectionString { get; set; }

        public string ExecutionEnvironmentId { get; set; }

        public List<ExecutionChainSettings> Chains { get; set; }

        public string AggregateID { get; set; }

        public CilaSettings()
        {
            Chains = new List<ExecutionChainSettings>();
        }
    }

    public class ExecutionChainSettings
    {
        public string ChainId { get; set; }

        public string ChainType { get; set; }

        public string Rpc { get; set; }

        public string PrivateKey { get; set; }

        public string EventStoreContract { get; set; }

        public string DispatcherContract { get; set; }
    }
}

