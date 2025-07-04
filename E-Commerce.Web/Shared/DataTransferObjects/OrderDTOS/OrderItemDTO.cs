namespace Shared.DataTransferObjects.OrderDTOS
{
    public class OrderItemDTO
    {
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
    }
}