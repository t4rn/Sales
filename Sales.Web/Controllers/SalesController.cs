﻿using Sales.DataLayer;
using Sales.Model;
using Sales.Web.Models;
using Sales.Web.ViewModels;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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

            SalesOrderViewModel vm = AppHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Customer has an Id = '{vm.Id}'.";

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

            SalesOrderViewModel vm = AppHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Editing Customer with Name = '{vm.CustomerName}' and Id = '{vm.Id}'";

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

            SalesOrderViewModel vm = AppHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            vm.MessageToClient = $"Deleting Customer with Name = '{vm.CustomerName}' and Id = '{vm.Id}'";
            vm.ObjectState = ObjectState.Deleted;

            return View(vm);
        }


        [HandleModelStateException]
        public JsonResult Save(SalesOrderViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                throw new ModelStateException(ModelState);
            }
            SalesOrder salesOrder = AppHelpers.CreateSalesOrderFromSalesOrderViewModel(vm);

            _salesContext.SalesOrders.Attach(salesOrder);

            if (salesOrder.ObjectState == ObjectState.Deleted)
            {
                foreach (SalesOrderItemViewModel orderItem in vm.SalesOrderItems)
                {
                    SalesOrderItem itemToDelete = _salesContext.SalesOrderItems.Find(orderItem.Id);
                    if (itemToDelete != null)
                    {
                        itemToDelete.ObjectState = ObjectState.Deleted;
                    }
                }
            }
            else
            {
                foreach (int itemId in vm.SalesOrderItemsToDelete)
                {
                    SalesOrderItem itemToDelete = _salesContext.SalesOrderItems.Find(itemId);
                    if (itemToDelete != null)
                    {
                        itemToDelete.ObjectState = ObjectState.Deleted;
                    }
                }
            }

            _salesContext.ApplyStateChanges();// .ChangeTracker.Entries<IObjectWithState>().Single().State = Helpers.ConvertState(salesOrder.ObjectState);

            string messageToClient = string.Empty;
            try
            {
                _salesContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                messageToClient = "Someone else have modified this sales order since you retrieved it.  Your changes have not been applied.  What you see now are the current values in the database.";
            }
            catch (Exception ex)
            {
                throw new ModelStateException(ex);
            }

            if (vm.ObjectState == ObjectState.Deleted)
            {
                return Json(new { newLocation = "/Sales/Index/" });
            }

            if (string.IsNullOrWhiteSpace(messageToClient))
            {
                messageToClient = AppHelpers.GetMessageToClient(vm.ObjectState, salesOrder.CustomerName, salesOrder.Id);
            }

            vm.Id = salesOrder.Id;
            _salesContext.Dispose();
            _salesContext = new SalesContext();
            salesOrder = _salesContext.SalesOrders.Find(vm.Id);

            vm = AppHelpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder); //.SalesOrderId = salesOrder.SalesOrderId;
            vm.MessageToClient = messageToClient;

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
