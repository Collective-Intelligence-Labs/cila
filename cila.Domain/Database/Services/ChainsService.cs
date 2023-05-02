using cila.Domain;
using cila.Domain.Database.Documents;
using cila.Domain.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class ChainsService
    {
        private readonly MongoDatabase database;

        public ChainsService(MongoDatabase database)
        {
            this.database = database;
        }

        public void InitializeFromSettings(CilaSettings settings)
        {
            var chains = GetAll();
            var chainsInSettings = settings.Chains;
            
            var chainsToAdd = chainsInSettings.Where(x => !chains.Select(c => c.ChainId).Contains(x.ChainId)).ToList();

            var chainsCollection = database.GetChainsCollection();

            foreach (var cta in chainsToAdd)
            {
                if (chains.Select(x => x.Id).Contains(cta.ChainId)) continue;

                chainsCollection.InsertOne(new ChainDocument
                {
                    Id = cta.ChainId,
                    ChainId = cta.ChainId,
                    PrivateKey = cta.PrivateKey,
                    DispatcherContract = cta.DispatcherContract,
                    RPC = cta.Rpc,
                    ChainType = cta.ChainType,
                    EventStoreContract = cta.EventStoreContract
                });
            }

            foreach (var chain in chainsInSettings)
            {
                if (chain.DispatcherContract != null)
                {
                    var update = Builders<ChainDocument>.Update.Set(x => x.DispatcherContract, chain.DispatcherContract);
                    chainsCollection.UpdateOne(x => x.ChainId == chain.ChainId && string.IsNullOrEmpty(x.DispatcherContract), update);
                }
                if (chain.EventStoreContract != null)
                {
                    var update = Builders<ChainDocument>.Update.Set(x => x.EventStoreContract, chain.EventStoreContract);
                    chainsCollection.UpdateOne(x => x.ChainId == chain.ChainId && string.IsNullOrEmpty(x.EventStoreContract), update);
                }
            }
        }

        public ChainDocument Get(string id)
        {
            var filter = Builders<ChainDocument>.Filter.Eq(x => x.Id, id);
            return database.GetChainsCollection().Find(filter).FirstOrDefault();
        }

        public List<ChainDocument> GetAll()
        {
            var filter = Builders<ChainDocument>.Filter.Empty;
            return database.GetChainsCollection().Find(filter).ToList();
        }
    }
}