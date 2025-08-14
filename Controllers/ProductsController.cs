using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
                return NotFound("No products found."); // <-- ส่ง 404
            return Ok(products); // <-- ส่ง 200 พร้อมข้อมูล
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // ส่ง 200 OK หรือ 201 Created แบบไม่ใช้ CreatedAtAction
            return Ok(product);
            // หรือถ้าอยาก 201:
            // return StatusCode(201, product);
        }

        //
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest("Id mismatch"); // ถ้า Id URL ไม่ตรงกับ body
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // ถ้าไม่เจอสินค้า
            }

            // อัพเดตข้อมูล
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();

            return Ok(product); // ส่ง 200 พร้อมข้อมูลที่อัพเดต
        }
        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // ถ้าไม่เจอสินค้า
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); // ส่ง 204 No Content
        }
        //
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchByName(string name)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(name)) // contains = like '%name%'
                .ToListAsync();

            if (!products.Any())
                return NotFound();

            return Ok(products);
        }
    }


}


