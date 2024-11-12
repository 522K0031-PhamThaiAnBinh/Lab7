namespace Lab7.Migrations
{
    using Lab7.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Lab7.Models.OrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lab7.Models.OrderContext context)
        {
            // Add sample Items
            var items = new[]
            {
        new Item { ItemName = "Item 1", UnitPrice = 10.00M },
        new Item { ItemName = "Item 2", UnitPrice = 20.00M },
        new Item { ItemName = "Item 3", UnitPrice = 15.00M }
    };
            context.Items.AddOrUpdate(i => i.ItemName, items);

            // Add sample Agents
            var agents = new[]
            {
        new Agent { AgentName = "Agent 1" },
        new Agent { AgentName = "Agent 2" }
    };
            context.Agents.AddOrUpdate(a => a.AgentName, agents);

            // Save changes to get IDs
            context.SaveChanges();

            // Add sample Orders and OrderDetails
            var order1 = new Order { OrderDate = DateTime.Now, AgentID = agents[0].AgentID };
            var order2 = new Order { OrderDate = DateTime.Now, AgentID = agents[1].AgentID };

            context.Orders.AddOrUpdate(o => o.OrderID, order1, order2);
            context.SaveChanges();

            // Add OrderDetails
            var orderDetails = new[]
            {
        new OrderDetail { OrderID = order1.OrderID, ItemID = items[0].ItemID, Quantity = 5, UnitAmount = items[0].UnitPrice },
        new OrderDetail { OrderID = order1.OrderID, ItemID = items[1].ItemID, Quantity = 3, UnitAmount = items[1].UnitPrice },
        new OrderDetail { OrderID = order2.OrderID, ItemID = items[0].ItemID, Quantity = 2, UnitAmount = items[0].UnitPrice },
        new OrderDetail { OrderID = order2.OrderID, ItemID = items[2].ItemID, Quantity = 4, UnitAmount = items[2].UnitPrice }
    };

            context.OrderDetails.AddOrUpdate(od => new { od.OrderID, od.ItemID }, orderDetails);
        }
    }
}
