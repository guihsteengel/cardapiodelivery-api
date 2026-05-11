using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Api.Data;
using FoodDelivery.Api.Models;
using FoodDelivery.Api.DTOs;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ POST - Criar pedido
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == dto.CartId);

            if (cart == null)
                return NotFound("Carrinho não encontrado");

            if (cart.Items == null || !cart.Items.Any())
                return BadRequest("Carrinho vazio");

            var address = new Address
            {
                Street = dto.Address.Street,
                Number = dto.Address.Number,
                City = dto.Address.City,
                ZipCode = dto.Address.ZipCode
            };

            var payment = new Payment
            {
                Method = dto.PaymentMethod,
                Status = "Pending"
            };

            var order = new Order
            {
                Address = address,
                Payment = payment,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList(),

                Total = cart.Items.Sum(i => i.Quantity * i.Product.Price),

                Status = "Pending",

                // 🔥 CORREÇÃO PRINCIPAL
                CreatedAt = DateTime.UtcNow,
                EstimatedTime = DateTime.UtcNow.AddMinutes(30)
            };

            _context.Addresses.Add(address);
            _context.Payments.Add(payment);
            _context.Orders.Add(order);

            // 🔥 LIMPA CARRINHO
            cart.Items.Clear();
            cart.Total = 0;

            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // ✅ GET - Listar pedidos
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.Payment)
                .Include(o => o.Address)
                .ToListAsync();

            return Ok(orders);
        }

        // ✅ PUT - Atualizar status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}