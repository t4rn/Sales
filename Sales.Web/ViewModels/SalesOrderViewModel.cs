using System;
using Sales.Model;
using System.Collections.Generic;

namespace Sales.Web.ViewModels
{
    public class SalesOrderViewModel : IObjectWithState
    {
        public SalesOrderViewModel()
        {
            SalesOrderItems = new List<SalesOrderItemViewModel>();
        }
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }

        public string MessageToClient { get; set; }

        public ObjectState ObjectState { get; set; }

        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }
    }
}
