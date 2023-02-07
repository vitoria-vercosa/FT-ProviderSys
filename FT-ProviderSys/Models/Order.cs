namespace FT_ProviderSys.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Code { get; set; }
        public DateTime OrderDate { get; set; }
        //public IEnumerable<int>? ProductIds { get; set; }
        public int ProviderId { get; set; }
        public double? Amount { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public Order()
        {

        }
        public Order(string code, int providerId, double amount)
        {
            this.Code = code;
            //this.ProductIds = productIds;
            this.ProviderId = providerId;
            this.Amount = amount;
            this.OrderDate = DateTime.UtcNow;
        }
    }
}
