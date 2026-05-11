namespace FoodDelivery.Api.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string Method { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
    }
}