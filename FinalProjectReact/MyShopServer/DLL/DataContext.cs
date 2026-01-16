
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLL
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> option)
        :base(option) {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FurnitureShopDB;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            {
                modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
                modelBuilder.Entity<User>()
                    .Property(u => u.UserName)
                    .HasMaxLength(20)
                    .IsRequired();
                modelBuilder.Entity<User>()
                    .Property(u => u.Email)
                    .HasMaxLength(50)
                    .IsRequired();
                modelBuilder.Entity<User>()
                    .Property(u => u.Password)
                    .IsRequired();
                modelBuilder.Entity<User>()
                    .Property(u => u.IsAdmin)
                    .IsRequired()
                    .HasDefaultValue(false);
                modelBuilder.Entity<User>()
                    .Property(u => u.IsBlocked)
                    .IsRequired()
                    .HasDefaultValue(false);
                modelBuilder.Entity<User>()
                    .HasOne(u => u.Cart)
                    .WithOne()
                    .HasForeignKey<User>(u => u.CartId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
            // CART \ CART_ITEM
            {
                modelBuilder.Entity<Cart>()
                    .HasKey(c => c.Id);

                modelBuilder.Entity<CartItem>()
                    .HasKey(ci => ci.Id);
                modelBuilder.Entity<CartItem>()
                    .HasOne(ci => ci.Cart)
                    .WithMany(c => c.Items)
                    .HasForeignKey(ci => ci.CartId);
                modelBuilder.Entity<CartItem>()
                    .HasOne(ci => ci.Product)
                    .WithMany()
                    .HasForeignKey(ci => ci.ProductId);
            }
            // CATEGORY
            {
                modelBuilder.Entity<Category>()
                    .HasKey(c => c.Id);
                modelBuilder.Entity<Category>()
                    .Property(c => c.Name)
                    .HasMaxLength(150)
                    .IsRequired();
            }
            // PRODUCT
            {
                modelBuilder.Entity<Product>()
                    .HasKey(p => p.Id);

                modelBuilder.Entity<Product>()
                    .Property(p => p.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(p => p.Description)
                    .HasMaxLength(1000)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasPrecision(18, 2)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(p => p.Discount)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(p => p.Count)
                    .IsRequired();

                modelBuilder.Entity<Product>()
                    .Property(p => p.Image)
                    .HasMaxLength(2000);
                modelBuilder.Entity<Product>()
                     .HasOne(p => p.Category)
                     .WithMany(c => c.Products)
                     .HasForeignKey(p => p.CategoryId)
                     .OnDelete(DeleteBehavior.Restrict);
            }

            // ADDRESS
            {
                modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

                modelBuilder.Entity<Address>()
                    .Property(a => a.City)
                    .HasMaxLength(30)
                    .IsRequired();

                modelBuilder.Entity<Address>()
                    .Property(a => a.Street)
                    .HasMaxLength(30)
                    .IsRequired();

                modelBuilder.Entity<Address>()
                    .Property(a => a.HouseNumber)
                    .HasMaxLength(5)
                    .IsRequired();

                modelBuilder.Entity<Address>()
                    .Property(a => a.PostalCode)
                    .IsRequired();
            }
            // ORDER
            {
                modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

                modelBuilder.Entity<Order>()
                    .Property(o => o.State)
                    .HasMaxLength(30)
                    .IsRequired();

                modelBuilder.Entity<Order>()
                    .Property(o => o.Comment)
                    .HasMaxLength(2000);

                modelBuilder.Entity<Order>()
                    .Property(o => o.Phone)
                    .HasMaxLength(15)
                    .IsRequired();
                modelBuilder.Entity<Order>()
                    .HasOne(o => o.Address)
                    .WithOne()
                    .HasForeignKey<Order>(o => o.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<Order>()
                    .HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
