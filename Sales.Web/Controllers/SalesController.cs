using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales.DataLayer;
using Sales.Model;
using Sales.Web.ViewModels;

namespace Sales.Web.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;

        public SalesController()
        {
            _salesContext = new SalesContext();
        }

        public ActionResult Index()
        {
            return View(_salesContext.SalesOrders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel vm = VmHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Customer has an Id = '{vm.SalesOrderId}'.";

            return View(vm);
        }

        public ActionResult Create()
        {
            SalesOrderViewModel vm = new SalesOrderViewModel() { MessageToClient = "You will create a new order!" };
            vm.ObjectState = ObjectState.Added;

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel vm = VmHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Editing Customer with Name = '{vm.CustomerName}' and Id = '{vm.SalesOrderId}'";
            vm.ObjectState = ObjectState.Unchanged;

            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel vm = VmHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Deleting Customer with Name = '{vm.CustomerName}' and Id = '{vm.SalesOrderId}'";
            vm.ObjectState = ObjectState.Deleted;

            return View(vm);
        }


        public JsonResult Save(SalesOrderViewModel vm)
        {
            SalesOrder salesOrder = VmHelpers.CreateSalesOrderFromSalesOrderViewModel(vm);
            salesOrder.ObjectState = vm.ObjectState;

            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = Helpers.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (vm.ObjectState == ObjectState.Deleted)
            {
                return Json(new { newLocation = "/Sales/Index/" });
            }

            vm.MessageToClient = VmHelpers.GetMessageToClient(vm.ObjectState, salesOrder.CustomerName, salesOrder.SalesOrderId);

            vm.SalesOrderId = salesOrder.SalesOrderId;
            vm.ObjectState = ObjectState.Unchanged;

            return Json(new { salesOrderViewModel = vm });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
