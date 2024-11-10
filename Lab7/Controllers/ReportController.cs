using Lab7.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // Add this

namespace Lab7.Controllers
{
    public class ReportController : Controller
    {
        private OrderContext db = new OrderContext();

        public ActionResult OrderReport(int id)
        {
            var order = db.Orders
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails.Select(od => od.Item))
                .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return new ViewAsPdf("OrderReport", order)
            {
                FileName = $"Order_{id}_Report.pdf"
            };
        }
    }
}