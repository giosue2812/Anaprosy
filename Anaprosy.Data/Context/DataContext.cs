using Anaprosy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anaprosy.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ProductDM> Products { get; set; }

        public DbSet<InventoryDM> Inventories { get; set; }

        public DbSet<InventoryItemDM> InventoryItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AnaprosyTestAppDatabase;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<ProductDM> products = new List<ProductDM>();

            for (int i = 1; i <= 25000; i++)
            {
                products.Add(new ProductDM
                {
                    Id = Guid.NewGuid(),
                    Name = "Product - " + i
                });
            }

            modelBuilder.Entity<ProductDM>().HasData(products);
        }
    }
}
