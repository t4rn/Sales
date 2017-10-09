namespace Sales.DataLayer.Migrations
{
    using Model;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Sales.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                s=> s.CustomerName,
                new SalesOrder { CustomerName = "Adam", PONumber = "123" },
                new SalesOrder { CustomerName = "John", PONumber = "234" },
                new SalesOrder { CustomerName = "Anna", PONumber = "456" }
                );
        }
    }
}
