namespace cila.Domain.Database.Documents
{
    public class MarketDocument
    {
        public string Id {get;set;}

        public string Owner {get;set;}

        public AssetItem Asset1 {get;set;}

        public AssetItem Asset2 {get;set;}

        public List<TradeHistoryItem> TradeHistory {get;set;}

        public MarketDocument()
        {
            TradeHistory = new List<TradeHistoryItem>();
        }
    }

    public class AssetItem
    {
        public string Symbol {get;set;}

        public ulong Value {get;set;}

        public AssetItem()
        {
            
        }

        public AssetItem(string sybmol, ulong value)
        {
            Symbol = sybmol;
            Value = value;
        }
    }

    public class TradeHistoryItem
    {
        public DateTime Timestamp {get;set;}

        public string Account {get;set;}

        public string AssetFrom{get;set;}

        public string AssetTo{get;set;}

        public ulong ValueFrom {get;set;}
        
        public ulong ValueTo {get;set;}
    }
}