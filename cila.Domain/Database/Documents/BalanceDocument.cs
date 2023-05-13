namespace cila.Domain.Database.Documents
{
    public class BalanceDocument
    {
        public string Id {get;set;}

        public string Asset {get;set;}

        public ulong Balance {get;set;}

        public string Account {get;set;}

        public string DaoId {get;set;}
    }
}