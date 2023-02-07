namespace FT_ProviderSys.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrderProduct()
        {

        }
        public OrderProduct(int orderId, int productId, int productQuantity)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            ProductQuantity = productQuantity;
        }
    }
}
