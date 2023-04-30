using cila.Domain.Database.Documents;
using MongoDB.Driver;

namespace cila.Domain.Database.Services
{
    public class NftService
    {
        private readonly IMongoCollection<NFTDocument> nfts;

        public NftService(MongoDatabase database)
        {
            nfts = database.GetNftsCollection();
        }

        public NFTDocument FindOneNft(string id)
        {
            var filter = Builders<NFTDocument>.Filter.Eq(x=> x.Id, id);
            return nfts.Find(filter).FirstOrDefault();
        }

        public IEnumerable <NFTDocument> FindAllNfts(string ownerId)
        {
            var filter = Builders<NFTDocument>.Filter.Eq(x=> x.Owner, ownerId);
            return nfts.Find(filter).ToList();
        }

        public IEnumerable <NFTDocument> FindAllNfts()
        {
            var filter = Builders<NFTDocument>.Filter.Empty;
            return nfts.Find(filter).ToList();
        }
    }
}