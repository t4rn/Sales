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
                s => s.CustomerName,
                new SalesOrder
                {
                    CustomerName = "Adam",
                    PONumber = "123",
                    SalesOrderItems =
                    {
                        new SalesOrderItem { ProductCode = "OnePlus", Quantity = 2, UnitPrice = 2000.33M },
                        new SalesOrderItem { ProductCode = "Samsung", Quantity = 1, UnitPrice = 3000.55M },
                        new SalesOrderItem { ProductCode = "Nokia", Quantity = 7, UnitPrice = 200.59M },
                    },
                },
                new SalesOrder { CustomerName = "John", PONumber = "234" },
                new SalesOrder
                {
                    CustomerName = "Anna",
                    PONumber = "456",
                    SalesOrderItems =
                    {
                        new SalesOrderItem { ProductCode = "ABC", Quantity = 12, UnitPrice = 3500.33M },
                        new SalesOrderItem { ProductCode = "XYZ", Quantity = 5, UnitPrice = 32300.55M },
                        new SalesOrderItem { ProductCode = "RET", Quantity = 8, UnitPrice = 670.59M },
                    },
                }
                );
        }
    }
}
