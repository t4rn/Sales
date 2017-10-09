using Sales.Model;
using System.Data.Entity.ModelConfiguration;

namespace Sales.DataLayer
{
    public class SalesOrderItemConfiguration : EntityTypeConfiguration<SalesOrderItem>
    {
        public SalesOrderItemConfiguration()
        {
            Property(i => i.ProductCode).HasMaxLength(15).IsRequired();
            Property(i => i.Quantity).IsRequired();
            Property(i => i.UnitPrice).IsRequired();
            Ignore(i => i.ObjectState);
        }
    }
}
