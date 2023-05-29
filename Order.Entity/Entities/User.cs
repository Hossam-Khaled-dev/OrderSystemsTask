using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Order.Entity.Entities;

public partial class User:IdentityUser<int>
{

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
