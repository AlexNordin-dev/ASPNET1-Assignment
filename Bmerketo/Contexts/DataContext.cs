using Bmerketo.Models;
using Bmerketo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bmerketo.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Product_ColorEntity___________________
            modelBuilder.Entity<Product_ColorEntity>().HasKey(pc => new
            {
                pc.ColorEntityId,
                pc.ProductEntityId
            });
            modelBuilder.Entity<Product_ColorEntity>().HasOne(p => p.ProductEntity).WithMany(pc => pc.Product_ColorEntity).HasForeignKey(p => p.ProductEntityId);
            modelBuilder.Entity<Product_ColorEntity>().HasOne(p => p.ColorEntity).WithMany(pc => pc.Product_ColorEntity).HasForeignKey(p => p.ColorEntityId);

            base.OnModelCreating(modelBuilder);
        }

        //Models
        public DbSet<BrandEntity> BrandEntity { get; set; }
        public DbSet<CategoryEntity> CategoryEntity { get; set; }
        public DbSet<ColorEntity> ColorEntity { get; set; }
        //public DbSet<ImageEntity> ImageEntity { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<ReviewEntity> ReviewEntity { get; set; }
        public DbSet<Product_ColorEntity> Product_ColorEntity { get; set; }
    }
}
