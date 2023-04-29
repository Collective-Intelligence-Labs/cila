using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Infrastructure.Chains;
using cila.Domain.Routers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class ExecutionsService
    {
        private readonly MongoDatabase database;

        public ExecutionsService(MongoDatabase database)
        {
            this.database = database;
        }

        public ExecutionDocument GetLastFor(string chainId, string aggregateId)
        {
            return database.GetExecutionsCollection()
                .Find(x => x.ChainId == chainId && x.AggregateId == aggregateId)
                .SortByDescending(x => x.Timestamp)
                .FirstOrDefault();
        }

        public void Record(string operationId, string chainId, ChainResponse response, RoutingStrategy strategy, string router)
        {
            database.GetExecutionsCollection().InsertOne(new ExecutionDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                OperationId = operationId,
                ChainId = chainId,
                ActualCost = response.GasUsed,
                RouterStrategy = strategy,
                RouterImplementation = router
            });
        }
    }
}

