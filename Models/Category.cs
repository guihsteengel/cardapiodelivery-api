using System.Text.Json.Serialization;

namespace FoodDelivery.Api.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonIgnore] // 🔥 EVITA LOOP
        public List<Product>? Products { get; set; }
    }
}