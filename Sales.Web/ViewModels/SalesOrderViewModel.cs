using System;
using Sales.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sales.Web.Models;

namespace Sales.Web.ViewModels
{
    public class SalesOrderViewModel : IObjectWithState
    {
        public SalesOrderViewModel()
        {
            SalesOrderItems = new List<SalesOrderItemViewModel>();
            SalesOrderItemsToDelete = new List<int>();
        }
        public int Id { get; set; }

        [CheckScore(3.14)]
        [Required(ErrorMessage = "Server: You cannot create a sales order unless you specify the customer's name.")]
        [StringLength(10, ErrorMessage = "Server: To long Customer's name.")]
        public string CustomerName { get; set; }

        [StringLength(10, ErrorMessage = "Server: Max 10 chars.")]
        public string PONumber { get; set; }

        public string MessageToClient { get; set; }

        public ObjectState ObjectState { get; set; }

        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }

        public List<int> SalesOrderItemsToDelete { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
