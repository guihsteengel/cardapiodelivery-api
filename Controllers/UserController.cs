using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Name == user.Name);

            if (existing != null)
                return Ok(existing);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}