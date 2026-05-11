using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}