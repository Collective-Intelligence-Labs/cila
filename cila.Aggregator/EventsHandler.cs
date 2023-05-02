
using System.Security.Cryptography;
using System.Text;
using cila.Domain;
using cila.Domain.Database;
using cila.Domain.Database.Documents;
using Google.Protobuf;
using MongoDB.Driver;
using Nethereum.Util;

namespace cila.Aggregator;

public class EventsHandler: IEventHandler
{
    private readonly MongoDatabase _database;

    public EventsHandler(MongoDatabase database)
    {
        _database = database;
    }

    public void Handle(NFTMintedPayload e){
        var nfts = _database.GetNftsCollection();
        var id = GetId(e.Hash, e.Owner);
        try
        {   
            nfts.InsertOne(new NFTDocument{
                Id = id,
                Hash = ByteStringToHexString(e.Hash),
                Owner = ByteStringToHexString(e.Owner)
            });
        }
        catch (MongoWriteException ex)
        {
            Console.WriteLine("Trying to create the same NFT " + id);
        }
    }

    public void Handle(NFTTransferedPayload e)
    {
        var nfts = _database.GetNftsCollection();
        var builder = Builders<NFTDocument>.Update.Set(x=> x.Owner, ByteStringToHexString(e.To));
        var hash = e.Hash.ToStringUtf8();
        var r = nfts.UpdateOne(x=> x.Hash == hash, builder);
    }

    private string GetId(ByteString hash, ByteString owner)
    {
        var inputBytes = hash.ToByteArray().Concat(owner.ToByteArray()).ToArray();
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    private string ByteStringToHexString(ByteString bs)
    {
        var bytes = bs.ToByteArray();
        return bytes.ByteArrayToHex(true);
    }
}