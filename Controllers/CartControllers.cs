using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = id,
                    Items = new List<CartItem>()
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return Ok(cart);
        }

        [HttpPost("{id}/add")]
        public async Task<IActionResult> AddToCart(int id, int productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = id,
                    Items = new List<CartItem>()
                };

                _context.Carts.Add(cart);
            }

            if (cart.Items == null)
                cart.Items = new List<CartItem>();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        [HttpPost("{id}/remove")]
        public async Task<IActionResult> RemoveFromCart(int id, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
                return NotFound();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            await _context.SaveChangesAsync();

            return Ok(cart);
        }
    }
}