using Sales.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Sales.DataLayer
{
    public class SalesOrderItemConfiguration : EntityTypeConfiguration<SalesOrderItem>
    {
        public SalesOrderItemConfiguration()
        {
            Property(i => i.SalesOrderId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_SalesOrderItem", 1) { IsUnique = true }));
            Property(i => i.ProductCode).HasMaxLength(15).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_SalesOrderItem", 2) { IsUnique = true }));
            Property(i => i.Quantity).IsRequired();
            Property(i => i.UnitPrice).IsRequired();
            Ignore(i => i.ObjectState);
        }
    }
}
