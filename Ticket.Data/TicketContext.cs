using Microsoft.EntityFrameworkCore;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext()
        {
        }

        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<CustomerOperationClaim> CustomerOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasKey(a => a.Id);
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
            modelBuilder.Entity<Customer>().Property(c => c.PasswordHash).IsRequired(false);
            modelBuilder.Entity<Customer>().Property(c => c.PasswordSalt).IsRequired(false);

            modelBuilder.Entity<Film>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Film>().Property(f => f.Name)
                                       .IsRequired()
                                       .HasMaxLength(50);
            modelBuilder.Entity<Film>().Property(f => f.Description)
                                       .HasMaxLength(200);

            modelBuilder.Entity<OperationClaim>().HasKey(o => o.Id);
            modelBuilder.Entity<OperationClaim>().Property(o => o.Id).ValueGeneratedOnAdd();

            //modelBuilder.Entity<CustomerOperationClaim>().Property(co => co.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CustomerOperationClaim>().HasKey(co => co.Id);

            modelBuilder.Entity<CustomerOperationClaim>().HasKey("CustomerId", "OperationClaimId");

            //Customer(M) - OperationClaim(M)
            modelBuilder.Entity<Customer>().HasMany(o => o.OperationClaims)
                                           .WithOne(o1 => o1.Customer)
                                           .HasForeignKey(o => o.CustomerId)
                                           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OperationClaim>().HasMany(c => c.Customers)
                                                 .WithOne(c1 => c1.OperationClaim)
                                                 .HasForeignKey(c => c.OperationClaimId)
                                                 .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<OperationClaim>().HasData
                (
                new OperationClaim { Id = 1, Name = "admin" }
                );
            modelBuilder.Entity<CustomerOperationClaim>().HasData
                (
                    new CustomerOperationClaim { Id = 1, CustomerId = 1, OperationClaimId = 1 }
                );
        }

    }
}
