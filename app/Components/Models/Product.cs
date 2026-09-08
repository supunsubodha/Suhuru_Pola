public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaximumNumberOfDays { get; set; }
    public Product(int id, string name, int maximumNumberOfDays)
    {
        Id = id;
        Name = name;
        MaximumNumberOfDays = maximumNumberOfDays;
    }
}