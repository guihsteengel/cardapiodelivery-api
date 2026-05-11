using System.Text.Json.Serialization;

namespace FoodDelivery.Api.Models {
    

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        // 🔗 RELACIONAMENTO
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

    
    }
}