
using System.Security.Cryptography;
using System.Text;
using cila.Domain;
using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using Google.Protobuf;
using MongoDB.Driver;
using Nethereum.Util;

namespace cila.Aggregator;

public class EventsHandler: IEventHandler
{
    private readonly MongoDatabase _database;
    private readonly MarketsService _markets;
    private readonly BalancesService _balances;

    public EventsHandler(MongoDatabase database, MarketsService markets, BalancesService balances)
    {
        _database = database;
        _markets = markets;
        _balances = balances;
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
        nfts.UpdateOne(x=> x.Id == GetId(e.Hash, e.From), builder);
    }

    public void Handle(AMMCreatedPayload e)
    {
        var doc = new MarketDocument{
            Id = e.AggregateId,
            Owner = ByteStringToHexString(e.Owner)
        };
        var asset1 = new AssetItem{
            Symbol = ByteStringToHexString(e.Asset1), 
            Value = e.Supply1
        };
        doc.Asset1 = new AssetItem(ByteStringToHexString(e.Asset1), e.Supply1);
        doc.Asset2 = new AssetItem(ByteStringToHexString(e.Asset2), e.Supply2);
        _markets.CreateNew(doc);
    }

    public void Handle(LiquidityAddedPayload e)
    {
        _markets.AddLiquidity(ByteStringToHexString(e.Account), e.Amount1, e.Amount2);
        var market = _markets.Get(e.AggregateId);
        var account = ByteStringToHexString(e.Account);
        _balances.RemoveBalance(account, e.AggregateId, market.Asset1.Symbol, e.Amount1);
        _balances.RemoveBalance(account, e.AggregateId, market.Asset2.Symbol, e.Amount2);
    }

    public void Handle(LiquidityRemovedPayload e)
    {
        _markets.RemoveLiquidity(e.AggregateId, e.Amount1, e.Amount2);
        var market = _markets.Get(e.AggregateId);
        var account = ByteStringToHexString(e.Account);
        _balances.AddBalance(account, e.AggregateId, market.Asset1.Symbol, e.Amount1);
        _balances.AddBalance(account, e.AggregateId, market.Asset2.Symbol, e.Amount2);
    }

    public void Handle(TokensSwapedPayload e)
    {
        var market = _markets.Get(e.AggregateId);
        var account = ByteStringToHexString(e.Account);
        _markets.SwapTokens(
            e.AggregateId,
            account, 
            ByteStringToHexString(e.AssetFrom), 
            ByteStringToHexString(e.AssetTo), 
            (ulong)e.AmountFrom,
            (ulong)e.AmountTo);
    
        _balances.RemoveBalance(account, e.AggregateId, ByteStringToHexString(e.AssetFrom), (ulong)e.AmountFrom);
        _balances.AddBalance(account, e.AggregateId, ByteStringToHexString(e.AssetTo), (ulong)e.AmountTo);
    }

    public void Handle(FundsWithdrawnPayload e)
    {
        var account = ByteStringToHexString(e.Account);
        _balances.RemoveBalance(account, e.AggregateId, ByteStringToHexString(e.Asset), e.Amount);
    }

    public void Handle(FundsDepositedPayload e)
    {
        var account = ByteStringToHexString(e.Account);
        _balances.AddBalance(account, e.AggregateId, ByteStringToHexString(e.Asset), e.Amount);
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
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}