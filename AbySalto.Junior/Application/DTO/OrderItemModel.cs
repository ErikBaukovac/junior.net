namespace AbySalto.Junior.Application.DTO
{
    public class OrderItemModel
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}