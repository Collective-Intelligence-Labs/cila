using cila.Domain.Database.Documents;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class MarketsService
    {
        private readonly MongoDatabase database;
        private readonly IMongoCollection<MarketDocument> _markets;

        public MarketsService(MongoDatabase database)
        {
            this.database = database;
            _markets = database.GetMarketsCollection();
        }

        public MarketDocument Get(string id)
        {
            var filter = Builders<MarketDocument>.Filter.Eq(x => x.Id, id);
            return database.GetMarketsCollection().Find(filter).FirstOrDefault();
        }

        public void CreateNew(MarketDocument doc)
        {
            database.GetMarketsCollection().InsertOne(doc);
        }

        public void AddLiquidity(string marketId, ulong amount1, ulong amount2)
        {
            var collection = database.GetMarketsCollection();
            var filter = Builders<MarketDocument>.Filter.Eq(x => x.Id, marketId);
            var doc = _markets.Find(filter).First();
            doc.Asset1.Value += amount1;
            doc.Asset2.Value += amount2;
            _markets.ReplaceOne(filter, doc);
        }

        public void RemoveLiquidity(string marketId, ulong amount1, ulong amount2)
        {
            var collection = database.GetMarketsCollection();
            var filter = Builders<MarketDocument>.Filter.Eq(x => x.Id, marketId);
            var doc = _markets.Find(filter).First();
            doc.Asset1.Value -= amount1;
            doc.Asset2.Value -= amount2;
            _markets.ReplaceOne(filter, doc);
        }

        public void SwapTokens(string marketId, string account, string fromAsset, string toAsset, ulong fromValue, ulong toValue)
        {
            var collection = database.GetMarketsCollection();
            var filter = Builders<MarketDocument>.Filter.Eq(x => x.Id, marketId);
            var doc = _markets.Find(filter).First();
            if (fromAsset == doc.Asset1.Symbol)
            {
                doc.Asset1.Value -= fromValue;
                doc.Asset2.Value += toValue;
            } else {
                doc.Asset2.Value -= fromValue;
                doc.Asset1.Value += toValue;
            }
            doc.TradeHistory.Add(new TradeHistoryItem{
                Account = account,
                AssetFrom = fromAsset,
                AssetTo = toAsset,
                ValueFrom = fromValue,
                ValueTo = toValue,
                Timestamp = DateTime.Now
            });
            _markets.ReplaceOne(filter, doc);
        }
    }
}

