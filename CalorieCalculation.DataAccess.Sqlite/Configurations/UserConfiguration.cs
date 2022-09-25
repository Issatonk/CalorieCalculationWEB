using CalorieCalculation.DataAccess.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCalculation.DataAccess.Sqlite.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(16).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(32).IsRequired();
            builder.Property(x=>x.Email).HasMaxLength(128).IsRequired();
        }
    }
}
