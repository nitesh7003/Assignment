using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleMVCAppDB.Data;
using SampleMVCAppDB.Models;

namespace SampleMVCAppDB.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult List(int page = 1, int pageSize = 10)
        {
            var data = db.Products
                .OrderBy(p => p.ProductID) // Add ordering for better performance
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Consider passing total count for pagination UI
            var totalCount = db.Products.Count();
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token validation
        public IActionResult Create(Product e)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(e);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(e);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = db.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token validation
        public IActionResult Edit(int id, Product e)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = e.Name;
                existingProduct.Price = e.Price;
                existingProduct.Description = e.Description;

                db.Products.Update(existingProduct);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(e);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Add anti-forgery token validation
        public IActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
