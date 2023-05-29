﻿namespace Order.Entity.ViewModels
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }
}
