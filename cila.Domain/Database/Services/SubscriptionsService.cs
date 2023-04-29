using cila.Domain.Database.Documents;
using cila.Domain.Database;
using MongoDB.Driver;
using MongoDB.Bson;

namespace cila.Domain.Database.Services
{
    public class SubscriptionsService
    {
        private readonly MongoDatabase database;

        public SubscriptionsService(MongoDatabase database)
        {
            this.database = database;

            var indexKeysDefinition = Builders<SubscriptionDocument>.IndexKeys.Ascending(e => e.AggregateId);
            var indexModel = new CreateIndexModel<SubscriptionDocument>(indexKeysDefinition);
            database.GetSubscriptionsCollection().Indexes.CreateOne(indexModel);
        }

        public void Create(string aggregateId, string chainId)
        {
            database.GetSubscriptionsCollection().InsertOne(new SubscriptionDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ChainId = chainId,
                AggregateId = aggregateId
            });
        }

        public SubscriptionDocument Get(string id)
        {
            var filter = Builders<SubscriptionDocument>.Filter.Eq(x => x.Id, id);
            return database.GetSubscriptionsCollection().Find(filter).FirstOrDefault();
        }

        public List<SubscriptionDocument> GetAll()
        {
            var filter = Builders<SubscriptionDocument>.Filter.Empty;
            return database.GetSubscriptionsCollection().Find(filter).ToList();
        }

        public IEnumerable<SubscriptionDocument> GetAllExceptOrigin(string aggregateId, string originChainId)
        {
            var filter = Builders<SubscriptionDocument>.Filter.And(
               Builders<SubscriptionDocument>.Filter.Eq(e => e.AggregateId, aggregateId),
               Builders<SubscriptionDocument>.Filter.Ne(e => e.ChainId, originChainId)
               );
            return database.GetSubscriptionsCollection().Find(filter).ToList();
        }


        public IEnumerable<SubscriptionDocument> GetAllFor(string aggregateId)
        {
            var filter = Builders<SubscriptionDocument>.Filter.And(
               Builders<SubscriptionDocument>.Filter.Eq(e => e.AggregateId, aggregateId)
               );
            return database.GetSubscriptionsCollection().Find(filter).ToList();
        }
    }
}

