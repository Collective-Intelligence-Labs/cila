using cila.Domain.Database.Documents;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class BalancesService
    {
        private readonly MongoDatabase database;
        private readonly IMongoCollection<BalanceDocument> _col;

        public BalancesService(MongoDatabase database)
        {
            this.database = database;
            _col = database.GetBalancesCollection();
        }

        public List<BalanceDocument> GetAll()
        {
            var filter = Builders<BalanceDocument>.Filter.Empty;
            return _col.Find(filter).ToList();
        }

        public List<BalanceDocument> GetAllAccountBalances(string account)
        {
            var filter = Builders<BalanceDocument>.Filter.Eq(x => x.Account, account);
            return _col.Find(filter).ToList();
        }

        public List<BalanceDocument> GetAllDaoBalances(string daoId)
        {
            var filter = Builders<BalanceDocument>.Filter.Eq(x => x.DaoId, daoId);
            return _col.Find(filter).ToList();
        }

        public List<BalanceDocument> GetAllAccountBalancesInDao(string accountId, string daoId)
        {
            var filterAnd = Builders<BalanceDocument>.Filter.And(
                Builders<BalanceDocument>.Filter.Eq(x => x.DaoId, daoId), 
                Builders<BalanceDocument>.Filter.Eq(x => x.Account, accountId));
            return _col.Find(filterAnd).ToList();
        }

        public void AddBalance(string account, string daoId, string asset, ulong amount)
        {
            var filterAnd = Builders<BalanceDocument>.Filter.And(
                Builders<BalanceDocument>.Filter.Eq(x => x.DaoId, daoId),
                Builders<BalanceDocument>.Filter.Eq(x => x.Account, account),
                Builders<BalanceDocument>.Filter.Eq(x => x.Asset, asset)
            );
            var doc = _col.Find(filterAnd).First();
            _col.UpdateOne(filterAnd, Builders<BalanceDocument>.Update.Inc(x=> x.Balance, amount));
        }


        public void RemoveBalance(string account, string daoId, string asset, ulong amount)
        {
            var filterAnd = Builders<BalanceDocument>.Filter.And(
                Builders<BalanceDocument>.Filter.Eq(x => x.DaoId, daoId),
                Builders<BalanceDocument>.Filter.Eq(x => x.Account, account),
                Builders<BalanceDocument>.Filter.Eq(x => x.Asset, asset)
            );
            var doc = _col.Find(filterAnd).First();
            doc.Balance -= amount;
            _col.ReplaceOne(filterAnd, doc);
        }
    }
}

