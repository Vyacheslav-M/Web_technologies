using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        ProductContext db;
        public ProductsController(ProductContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpPost]
        public void PostProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        [HttpPut]
        public void PutProduct(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            foreach (var product in db.Products.ToList())
            {
                if (product.Id == id) db.Products.Remove(product);
            }
            db.SaveChanges();
        }
    }
}
