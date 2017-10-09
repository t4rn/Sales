namespace Sales.Web.ViewModels
{
    public class SalesOrderViewModel
    {
        public int SalesOrderId { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }

        public string MessageForClient { get; set; }
    }
}
