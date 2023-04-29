using cila.Domain.Database.Documents;
using cila.Domain.Database;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class SubscriptionsService
    {
        private readonly MongoDatabase database;

        public SubscriptionsService(MongoDatabase database)
        {
            this.database = database;
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
    }
}

