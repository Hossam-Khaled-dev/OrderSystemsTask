namespace Order.Entity.ViewModels
{
    public class OrderItemViewModel
    {

        public int Id { get; set; }

        public int OrderId { get; set; }
        public SellsmanViewModel Order { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Total { get; set; }
    }
}
