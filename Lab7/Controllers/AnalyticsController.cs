using Lab7.Models.ViewModels;
using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // Add this

namespace Lab7.Controllers
{
    public class AnalyticsController : Controller
    {
        private OrderContext db = new OrderContext();

        public ActionResult BestItems()
        {
            var bestItems = db.OrderDetails
                .GroupBy(od => od.ItemID)
                .Select(g => new BestItemViewModel
                {
                    Item = g.First().Item,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitAmount)
                })
                .OrderByDescending(x => x.TotalQuantity)
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
    }
}