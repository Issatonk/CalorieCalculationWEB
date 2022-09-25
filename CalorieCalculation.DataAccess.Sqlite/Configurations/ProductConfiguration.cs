﻿using CalorieCalculation.DataAccess.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieCalculation.DataAccess.Sqlite.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(32).IsRequired();
            //builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.PictureLink).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
        }
    }
}
