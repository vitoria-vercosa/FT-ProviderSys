using FT_ProviderSys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FT_ProviderSys.Data.Mappings
{
    public class OrderProductMapping : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("Order_Product");
            builder.HasKey(x => new { x.OrderId, x.ProductId } );
            builder.HasOne<Order>(x => x.Order).WithMany(x => x.OrderProducts).HasForeignKey(x => x.OrderId);
            builder.HasOne<Product>(x => x.Product).WithMany(x => x.OrderProducts).HasForeignKey(x => x.ProductId);
        }
    }
}
