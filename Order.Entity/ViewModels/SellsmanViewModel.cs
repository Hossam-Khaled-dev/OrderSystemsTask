namespace Order.Entity.ViewModels
{
    public class SellsmanViewModel
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
