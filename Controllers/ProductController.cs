using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.ImageUrl,
                    p.IsAvailable,
                    p.CategoryId,
                    Category = new
                    {
                        p.Category.Id,
                        p.Category.Name
                    }
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}