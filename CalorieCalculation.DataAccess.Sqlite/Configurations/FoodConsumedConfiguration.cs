using CalorieCalculation.DataAccess.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieCalculation.DataAccess.Sqlite.Configurations
{
    internal class FoodConsumedConfiguration : IEntityTypeConfiguration<FoodConsumed>
    {
        public void Configure(EntityTypeBuilder<FoodConsumed> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.Property(x => x.User).IsRequired();
            //builder.Property(x => x.Product).IsRequired();
            builder.Property(x=>x.Datetime).IsRequired();
        }
    }
}
