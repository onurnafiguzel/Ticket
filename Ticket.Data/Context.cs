using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Admin>().Property(a => a.Name)
                                           .IsRequired()
                                           .HasMaxLength(50);
            modelBuilder.Entity<Admin>().Property(a => a.Email)
                                           .IsRequired()
                                           .HasMaxLength(50);

            modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(c => c.Name)
                                           .IsRequired()
                                           .HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Email)
                                           .IsRequired()
                                           .HasMaxLength(50);

            modelBuilder.Entity<Film>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Film>().Property(f => f.Name)
                                       .IsRequired()
                                       .HasMaxLength(50);
            modelBuilder.Entity<Film>().Property(f => f.Description)
                                       .HasMaxLength(200);


            modelBuilder.Entity<Admin>().HasData
                (
                    new Admin { Id = 1, Name = "Enes Solak", Email = "enessolak.dev" },
                    new Admin { Id = 2, Name = "Onur Güzel", Email = "ongguzel@gmail.com" }
                );

            modelBuilder.Entity<Customer>().HasData
                (
                    new Customer { Id = 1, Name = "İbrahim Ertan Yılmaz", Email = "ibrahim@gmail.com" },
                    new Customer { Id = 2, Name = "Orhan İnaç", Email = "inac.orhan@outlook.com" }
                );

            modelBuilder.Entity<Film>().HasData
                (
                    new Film { Id = 1, Name = "Inception", Description = "A drea film.", Director = "Cristopher Nolan", Duration = 148, Rating = 8.8 },
                    new Film { Id = 2, Name = "Ahlat Ağacı", Description = "Turkish philosophy movie", Director = "Nuri Bilge Ceylan", Duration = 188, Rating = 8.1 }
                );
        }

    }
}
