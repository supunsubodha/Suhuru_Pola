public class Bid
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public DateTime Time { get; set; }
    public Bid(int id, Product product, User user, decimal amount, DateTime time)
    {
        Id = id;
        Product = product;
        User = user;
        Amount = amount;
        Time = time;
    }
}