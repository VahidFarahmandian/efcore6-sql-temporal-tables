namespace TemporalTable.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public override string ToString() => $"Name:{Name}, Price:{Price}";
    }
}
