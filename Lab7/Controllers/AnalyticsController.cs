using Lab7.Models.ViewModels;
using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Lab7.Controllers
{
    public class AnalyticsController : Controller
    {
        private OrderContext db = new OrderContext();

        public ActionResult BestItems()
        {
            var bestItems = db.OrderDetails
                .Include(od => od.Item)
                .Include(od => od.Order)
                .GroupBy(od => new { od.ItemID, od.Item.ItemName })
                .Select(g => new BestItemViewModel
                {
                    // Use FirstOrDefault to safely get the first item in the group
                    Item = g.FirstOrDefault().Item, // or g.First() if you expect at least one element in each group
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitAmount),
                    OrderCount = g.Select(od => od.OrderID).Distinct().Count() // Count distinct orders
                })
                .OrderByDescending(x => x.TotalRevenue) // Or OrderByDescending(x => x.OrderCount) depending on your preference
                .Take(10)
                .ToList();

            return View(bestItems);
        }

        public ActionResult AgentPurchases(int? agentId)
        {
            var agents = db.Agents.ToList();
            ViewBag.AgentID = new SelectList(agents, "AgentID", "AgentName");

            var query = db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Item));

            if (agentId.HasValue)
            {
                query = query.Where(o => o.AgentID == agentId.Value);
            }

            var purchases = query.ToList();
            return View(purchases);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}