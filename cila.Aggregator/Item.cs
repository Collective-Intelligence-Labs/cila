public class Item
{
    public string Hash {get;set;}
    public string Owner {get;set;}

    public Item(string hash, string owner)
    {
        Hash = hash;
        Owner = owner;
    }
}