using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lr2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lr2.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db;
        public HomeController(ProductContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [Authorize]
        public IActionResult Orders()
        {
            ViewBag.Order = db.Orders.ToList();
            return View(db.Products.ToList());
        }

        [Authorize]
        public IActionResult Sale()
        {
            return View(db.Products.ToList());
        }

        [Authorize]
        public IActionResult Buy(int id)
        {
            foreach (var product in db.Products.ToList())
            {
                if (product.User == User.Identity.Name && product.Id == id) return RedirectToAction("Index");
                if (product.Quantity == 0 && product.Id == id) return RedirectToAction("Index");
            }
            int f = 0;
            foreach (var Order in db.Orders.ToList())
            {
                if (Order.ProductId == id)
                {
                    Order.Quantity = ++Order.Quantity;
                    db.Orders.Update(Order);
                    f = 1;
                }
            }
            if (f == 0)
            {
                Order order = new Order();
                order.ProductId = id;
                order.UserId = User.Identity.Name;
                order.Quantity = 1;
                db.Orders.Add(order);
            }
            foreach (var product in db.Products.ToList())
            {
                if (product.Id == id) product.Quantity = --product.Quantity;
                db.Products.Update(product);
            }
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Add()
        {
            ViewBag.User = User.Identity.Name;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            db.Products.Add(product);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddQuantity(int id)
        {
            foreach (var product in db.Products.ToList())
            {
                if (product.Id == id && product.Quantity == 0) return RedirectToAction("Orders");
                if (product.Id == id) product.Quantity = --product.Quantity;
                db.Products.Update(product);
            }
            foreach (var Order in db.Orders.ToList())
            {
                if (Order.ProductId == id) Order.Quantity = ++Order.Quantity;
                db.Orders.Update(Order);
            }
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        [Authorize]
        public IActionResult RemoveQuantity(int id)
        {
            foreach (var Order in db.Orders.ToList())
            {
                if (Order.ProductId == id) Order.Quantity = --Order.Quantity;
                db.Orders.Update(Order);
                if (Order.ProductId == id && Order.Quantity == 0) db.Orders.Remove(Order);
            }
            foreach (var product in db.Products.ToList())
            {
                if (product.Id == id) product.Quantity = ++product.Quantity;
                db.Products.Update(product);
            }
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        [Authorize]
        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            foreach (var product in db.Products.ToList())
            {
                if (product.Id == id) ViewBag.Product = product;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            db.Products.Update(product);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Sale");
        }

        [Authorize]
        public IActionResult Delete(Product product)
        {
            db.Products.Remove(product);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Sale");
        }

        [Authorize]
        public IActionResult DeleteOrder(int? id)
        {
            foreach (var order in db.Orders.ToList())
            {
                if (order.ProductId == id)
                {
                    foreach (var product in db.Products.ToList())
                    {
                        if (product.Id == id) product.Quantity += order.Quantity;
                        db.Products.Update(product);
                    }
                    db.Orders.Remove(order);
                }
            }
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        [Authorize]
        public IActionResult DeleteOrders(int? id)
        {
            foreach (var order in db.Orders.ToList())
            {
                if (order.UserId == User.Identity.Name) db.Orders.Remove(order);
            }
            // сохраняем в бд все изменения
            db.SaveChanges();
            return RedirectToAction("Orders");
        }
    }
}