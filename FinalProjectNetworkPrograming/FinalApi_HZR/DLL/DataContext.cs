using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
        {
        }
        public DataContext() 
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CryptoMonetDB;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CryptoMonet>()
         .HasKey(x => x.id);

            modelBuilder.Entity<CryptoMonet>()
                .Property(x => x.name)
                .IsRequired();

            modelBuilder.Entity<CryptoMonet>()
                .Property(x => x.symbol)
                .IsRequired();

            modelBuilder.Entity<CryptoMonet>()
                .HasIndex(x => x.symbol)
                .IsUnique();

            modelBuilder.Entity<CryptoMonet>()
                .HasOne(x => x.quote)
                .WithOne()
                .HasForeignKey<Quote>(x => x.id);

            modelBuilder.Entity<Quote>()
                .HasKey(x => x.id);

            modelBuilder.Entity<Quote>()
                .HasOne(x => x.USD)
                .WithOne()
                .HasForeignKey<QuoteName>(x => x.id);

            modelBuilder.Entity<QuoteName>()
                .HasKey(x => x.id);

            modelBuilder.Entity<QuoteName>()
                .Property(x => x.price)
                .IsRequired();


        }
    }
}
