namespace cila.Domain.Database.Documents
{
    public class ChainDocument
    {
        public string Id {get;set;}

        public string ChainId { get; set; }

        public string ChainType {get;set;}

        public string RPC {get;set;}

        public string PrivateKey {get;set;}

        public string Symbol {get;set;}

        public string DispatcherContract {get;set;}

        public string EventStoreContract { get; set; }
    }
}