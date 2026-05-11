namespace FoodDelivery.Api.DTOs
{
    public class CreateOrderDto
    {
        public int CartId { get; set; }

        public AddressDto Address { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}