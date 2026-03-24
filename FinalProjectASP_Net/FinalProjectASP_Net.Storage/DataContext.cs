using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Storage
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserBase> UserBases { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HRUser> HRUsers { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Application> Applications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // USER INHERITANCE (TPH)

            modelBuilder.Entity<UserBase>()
                .HasDiscriminator<Role>("Role")
                .HasValue<Admin>(Role.Admin)
                .HasValue<HRUser>(Role.HR)
                .HasValue<Employee>(Role.Employee);

            modelBuilder.Entity<UserBase>()
                .HasIndex(u => u.Email)
                .IsUnique();


            // HR -> Company (1:1)

            modelBuilder.Entity<HRUser>()
                .HasOne(hr => hr.Company)
                .WithMany() 
                .HasForeignKey(hr => hr.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);


            // Company -> Vacancies (1:N)

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Vacancies)
                .WithOne(v => v.Company)
                .HasForeignKey(v => v.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);


            // Vacancy -> Applications (1:N)

            modelBuilder.Entity<Vacancy>()
                .HasMany(v => v.Applications)
                .WithOne(a => a.Vacancy)
                .HasForeignKey(a => a.VacancyId)
                .OnDelete(DeleteBehavior.Cascade);


            // Employee -> Applications (1:N)

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Applications)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // Application date default

            modelBuilder.Entity<Application>()
                .Property(a => a.ApplicationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            //Application status default
            modelBuilder.Entity<Application>()
                .Property(a => a.Status)
                .HasDefaultValue(ApplicationStatus.Pending);
        }
    }
}
