using Sales.Model;
using Sales.Web.ViewModels;

namespace Sales.Web.Models
{
    public static class AppHelpers
    {
        public static SalesOrderViewModel CreateSalesOrderViewModelFromSalesOrder(SalesOrder salesOrder)
        {
            SalesOrderViewModel vm = new SalesOrderViewModel();
            vm.Id = salesOrder.Id;
            vm.CustomerName = salesOrder.CustomerName;
            vm.PONumber = salesOrder.PONumber;
            vm.ObjectState = ObjectState.Unchanged; // it's always Unchanged

            foreach (SalesOrderItem salesOrderItem in salesOrder.SalesOrderItems)
            {
                SalesOrderItemViewModel itemVm = new SalesOrderItemViewModel();
                itemVm.Id = salesOrderItem.Id;
                itemVm.ProductCode = salesOrderItem.ProductCode;
                itemVm.Quantity = salesOrderItem.Quantity;
                itemVm.UnitPrice = salesOrderItem.UnitPrice;

                itemVm.ObjectState = ObjectState.Unchanged;

                itemVm.SalesOrderId = salesOrder.Id;

                vm.SalesOrderItems.Add(itemVm);
            }

            return vm;
        }

        public static SalesOrder CreateSalesOrderFromSalesOrderViewModel(SalesOrderViewModel vm)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.Id = vm.Id;
            salesOrder.CustomerName = vm.CustomerName;
            salesOrder.PONumber = vm.PONumber;
            salesOrder.ObjectState = vm.ObjectState;

            int tempSalesOrderItemId = -1;
            foreach (var orderItem in vm.SalesOrderItems)
            {
                SalesOrderItem s = new SalesOrderItem()
                {
                    SalesOrderId = vm.Id,
                    ProductCode = orderItem.ProductCode,
                    Quantity = orderItem.Quantity,
                    ObjectState = orderItem.ObjectState,
                    UnitPrice = orderItem.UnitPrice,
                };

                if (orderItem.ObjectState != ObjectState.Added)
                {
                    s.Id = orderItem.Id;
                }
                else
                {
                    s.Id = tempSalesOrderItemId;
                    tempSalesOrderItemId--;
                }

                salesOrder.SalesOrderItems.Add(s);
            }

            return salesOrder;
        }

        public static string GetMessageToClient(ObjectState objectState, string customerName, int id)
        {
            string messageToClient = string.Empty;

            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = $"{customerName}'s order has been added to DB with Id = {id}.";
                    break;
                case ObjectState.Modified:
                    messageToClient = $"{customerName}'s order has been modified to DB with Id = {id}.";
                    break;
                case ObjectState.Unchanged:
                    messageToClient = $"{customerName}'s order has no changes...";
                    break;
                default:
                    messageToClient = $"Unknown state = '{objectState}'";
                    break;
            }

            return messageToClient;
        }
    }
}
