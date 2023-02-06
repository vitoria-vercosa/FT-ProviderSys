namespace FT_ProviderSys.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; init; } = DateTime.UtcNow;
        public float Price { get; set; }

        public Product()
        {

        }

        public Product(string code, string productName, string description, float price)
        {
            this.Code = code;
            this.ProductName = productName;
            this.Description = description;
            this.Price = price;
        }
    }
}
