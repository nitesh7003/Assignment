using Microsoft.AspNetCore.Mvc;
using RESTfulAPIs.Data;
using RESTfulAPIs.Models;

namespace RESTfulAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public ProductApiController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {
            var data = db.Products.ToList();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddProduct(Product e)
        {
            var data = db.Products.Add(e);
            if (data == null)
            {
                return NotFound();
            }
            db.SaveChanges();
            return Ok("Added Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product e)
        {
            var data = db.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            data.Name = e.Name;
            data.Price = e.Price;
            data.Description = e.Description;

            db.Products.Update(data);
            db.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = db.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            db.Products.Remove(data);
            db.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}
