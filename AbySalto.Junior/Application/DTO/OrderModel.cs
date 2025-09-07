namespace AbySalto.Junior.Application.DTO
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? Comments { get; set; }
        public decimal? TotalValue { get; set; }

        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        /* public string CustomerStreet { get; set; } = string.Empty;
         public string? BuildingNo { get; set; }
         public string? ApartmentNo { get; set; }
         public string? PostCode { get; set; }
         public string CustomerCity { get; set; } = string.Empty;*/
        public string? PhoneNumber { get; set; }
        public string Currency { get; set; } = string.Empty;

        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}
