using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;


namespace Order.Entity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<OrderDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
      b => b.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName)));


          
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}