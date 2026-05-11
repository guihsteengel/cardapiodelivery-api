using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> Create(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return Ok(restaurant);
        }
    }
}