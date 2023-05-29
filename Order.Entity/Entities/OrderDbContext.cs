using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Order.Entity.Entities;

public partial class OrderDbContext :  IdentityDbContext<User, Role, int>
{
 

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
}
