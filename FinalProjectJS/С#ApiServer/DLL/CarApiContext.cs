using Microsoft.EntityFrameworkCore;
using BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    
    public class CarApiContext : DbContext
    {
        public CarApiContext(DbContextOptions<CarApiContext> options)
        : base(options)
        {

        }
        public CarApiContext()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarApiDB;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Car>()
                .HasKey(x => x.id);

            modelBuilder.Entity<Car>()
                .Property(x => x.id)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(x => x.make)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(x => x.model)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(x => x.year)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(x => x.mileage)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(x => x.description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Car>()
                .Property(x => x.type)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Car>()
                .Property(x => x.price)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(x => x.discount)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(x => x.imageUrl)
                .HasMaxLength(1000);
        }
    }
}
