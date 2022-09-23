using CalorieCalculation.DataAccess.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculation.DataAccess.Sqlite
{
    public class CalorieCalculationContext : DbContext
    {
        public CalorieCalculationContext(DbContextOptions<CalorieCalculationContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FoodConsumed> FoodsConsumed { get; set; }
    }
}