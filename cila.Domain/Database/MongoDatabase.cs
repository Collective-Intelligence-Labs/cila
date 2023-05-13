using cila.Domain;
using cila.Domain.Database.Documents;
using MongoDB.Driver;

namespace cila.Domain.Database {

    public class MongoDatabase
    {
        private MongoClient _client;

        private const string RelayDatabaseName = "cila-relay";
        private const string ReadmodelDatabaseName = "cila-readmodel";

        private class Collections {
            // Aggregator / read-model
            public static string Chains = "chains";
            public static string AggregatedEvents = "aggregated-events";
            public static string Operations = "operations";
            public static string Nfts = "nfts";
            public static string Markets = "markets";
            public static string Balances = "balances";

            // Relay
            public static string Events  = "events";
            public static string Subscriptions  = "subscriptions";

            // Router
            public static string Executions  = "executions";            
        }

        public MongoDatabase(CilaSettings settings)
        {
            _client = new MongoClient(settings.MongoDBConnectionString);
        }


        // Aggregator / read-model
        public IMongoCollection<ChainDocument> GetChainsCollection()
        {
            return _client.GetDatabase(ReadmodelDatabaseName).GetCollection<ChainDocument>(Collections.Chains);
        }

        public IMongoCollection<AggregatedEventDocument> GetAggregatedEventsCollection()
        {
            return _client.GetDatabase(ReadmodelDatabaseName).GetCollection<AggregatedEventDocument>(Collections.AggregatedEvents);
        }

        public IMongoCollection<OperationDocument> GetOperationsCollection()
        {
            return _client.GetDatabase(ReadmodelDatabaseName).GetCollection<OperationDocument>(Collections.Operations);
        }

        public IMongoCollection<NFTDocument> GetNftsCollection()
        {
            return _client.GetDatabase(ReadmodelDatabaseName).GetCollection<NFTDocument>(Collections.Nfts);
        }

        public IMongoCollection<BalanceDocument> GetBalancesCollection()
        {
            return _client.GetDatabase(ReadmodelDatabaseName).GetCollection<BalanceDocument>(Collections.Balances);
        }

        public IMongoCollection<MarketDocument> GetMarketsCollection()
        {
            return _client.GetDatabase(RelayDatabaseName).GetCollection<MarketDocument>(Collections.Markets);
        }

        // Relay
        public IMongoCollection<SubscriptionDocument> GetSubscriptionsCollection()
        {
            return _client.GetDatabase(RelayDatabaseName).GetCollection<SubscriptionDocument>(Collections.Subscriptions);
        }

        public IMongoCollection<ExecutionChainEventDocument> GetEventsCollection()
        {
            return _client.GetDatabase(RelayDatabaseName).GetCollection<ExecutionChainEventDocument>(Collections.Events);
        }

        
        // Router
        public IMongoCollection<ExecutionDocument> GetExecutionsCollection()
        {
            return _client.GetDatabase(RelayDatabaseName).GetCollection<ExecutionDocument>(Collections.Executions);
        }

        
    }
}
