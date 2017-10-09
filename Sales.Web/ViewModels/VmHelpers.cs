using Sales.Model;

namespace Sales.Web.ViewModels
{
    public static class VmHelpers
    {
        public static SalesOrderViewModel CreateSalesOrderViewModelFromSalesOrder(SalesOrder salesOrder)
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            salesOrderViewModel.PONumber = salesOrder.PONumber;

            return salesOrderViewModel;
        }

        public static SalesOrder CreateSalesOrderFromSalesOrderViewModel(SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.SalesOrderId = salesOrderViewModel.SalesOrderId;
            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;

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
            }

            return messageToClient;
        }
    }
}
