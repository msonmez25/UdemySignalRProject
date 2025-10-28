namespace SignalRWebUI.Dtos.OrderDetailDtos
{
    public class ResultOrderDetailGetByOrderIdDto
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // Opsiyonel (Order ilişkisi için)
        public DateTime OrderDate { get; set; }
        public string TableNumber { get; set; }
    }
}
