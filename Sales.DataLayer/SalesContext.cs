using Sales.Model;
using System.Data.Entity;

namespace Sales.DataLayer
{
    public class SalesContext : DbContext
    {
        public SalesContext() : base("DefaultConnection")
        {

        }

        public DbSet<SalesOrder> SalesOrders { get; set; }
    }
}
