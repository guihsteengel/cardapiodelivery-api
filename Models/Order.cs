namespace FoodDelivery.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items { get; set; } = new();

        public decimal Total { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AddressId { get; set; }

        public Address Address { get; set; } = null!;

        public int PaymentId { get; set; }

        public Payment Payment { get; set; } = null!;

        public DateTime EstimatedTime { get; set; }

    }
}