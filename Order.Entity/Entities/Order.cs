using System;
using System.Collections.Generic;

namespace Order.Entity.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;

   
}
