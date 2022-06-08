using Microsoft.EntityFrameworkCore;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<CustomerOperationClaim> CustomerOperationClaims { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieSession> MovieSessions { get; set; }
        public DbSet<Theather> Theathers { get; set; }
        public DbSet<TheatherSeat> TheatherSeats { get; set; }
        public DbSet<MovieSessionSeat> MovieSessionSeats { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<TheatherPrice> TheatherPrices { get; set; }
        public DbSet<Domain.Entities.Concrete.Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Özellikler
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

            modelBuilder.Entity<Movie>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movie>().Property(f => f.Title)
                                       .IsRequired()
                                       .HasMaxLength(200);
            modelBuilder.Entity<Movie>().Property(f => f.OriginalTitle)
                                      .IsRequired()
                                      .HasMaxLength(200);
            //modelBuilder.Entity<Movie>().Property(f => f.Description)
            //                           .HasMaxLength(500);
            modelBuilder.Entity<Movie>().Property(f => f.PosterPath)
                                       .HasMaxLength(100);
            modelBuilder.Entity<Movie>().Property(f => f.BackdropPath)
                                       .HasMaxLength(100);
            modelBuilder.Entity<Movie>().Property(f => f.OriginalLanguage)
                                       .HasMaxLength(50);
            modelBuilder.Entity<Movie>().Property(f => f.Status)
                                       .HasMaxLength(50);
            modelBuilder.Entity<Movie>().Property(f => f.TrailerUrl)
                                       .HasMaxLength(50);
            modelBuilder.Entity<Movie>().Property(f => f.Director)
                                       .HasMaxLength(100);
            modelBuilder.Entity<Movie>().Property(f => f.ImdbId)
                                       .HasMaxLength(50);

            modelBuilder.Entity<OperationClaim>().HasKey(o => o.Id);
            modelBuilder.Entity<OperationClaim>().Property(o => o.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<CustomerOperationClaim>().HasKey(co => co.Id);
            modelBuilder.Entity<CustomerOperationClaim>().Property(co => co.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<CustomerOperationClaim>().HasKey("CustomerId", "OperationClaimId");

            modelBuilder.Entity<Actor>().HasKey(a => a.Id);
            modelBuilder.Entity<Actor>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Actor>().Property(a => a.Name)
                                        .HasMaxLength(200);
            modelBuilder.Entity<Actor>().Property(a => a.ProfilePath)
                                        .HasMaxLength(200);

            modelBuilder.Entity<Cast>().HasKey(a => a.Id);
            modelBuilder.Entity<Cast>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Cast>().Property(c => c.Character)
                                       .HasMaxLength(200);

            modelBuilder.Entity<Genre>().HasKey(a => a.Id);
            modelBuilder.Entity<Genre>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Genre>().Property(c => c.Name)
                                     .HasMaxLength(200);

            modelBuilder.Entity<MovieGenre>().HasKey(a => a.Id);
            modelBuilder.Entity<MovieGenre>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<MovieSession>().HasKey(a => a.Id);
            modelBuilder.Entity<MovieSession>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MovieSession>().Property(c => c.Name)
                                    .HasMaxLength(200);

            modelBuilder.Entity<Theather>().HasKey(a => a.Id);
            modelBuilder.Entity<Theather>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Theather>().Property(c => c.Name)
                                    .HasMaxLength(200);

            modelBuilder.Entity<MovieSessionSeat>().HasKey(a => a.Id);
            modelBuilder.Entity<MovieSessionSeat>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TheatherSeat>().HasKey(a => a.Id);
            modelBuilder.Entity<TheatherSeat>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TheatherSeat>().Property(c => c.Name)
                                    .HasMaxLength(200);

            modelBuilder.Entity<City>().HasKey(a => a.Id);
            modelBuilder.Entity<City>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Place>().HasKey(p => p.Id);
            modelBuilder.Entity<Place>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<TheatherPrice>().HasKey(p => p.Id);
            modelBuilder.Entity<TheatherPrice>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Entities.Concrete.Ticket>().HasKey(p => p.Id);
            modelBuilder.Entity<Domain.Entities.Concrete.Ticket>().Property(p => p.Id).ValueGeneratedOnAdd();

            // İlişkiler

            //Customer(M) - OperationClaim(M)
            modelBuilder.Entity<Customer>().HasMany(o => o.OperationClaims)
                                           .WithOne(o1 => o1.Customer)
                                           .HasForeignKey(o => o.CustomerId)
                                           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OperationClaim>().HasMany(c => c.Customers)
                                                 .WithOne(c1 => c1.OperationClaim)
                                                 .HasForeignKey(c => c.OperationClaimId)
                                                 .OnDelete(DeleteBehavior.NoAction);

            // Cast(M) - Actor(1)
            modelBuilder.Entity<Actor>().HasMany(c => c.Casts)
                                        .WithOne(o => o.Actor)
                                        .HasForeignKey(o => o.ActorId)
                                        .OnDelete(DeleteBehavior.NoAction);

            // Movie(M) - Genre(M)
            modelBuilder.Entity<Movie>().HasMany(c => c.Genres)
                                        .WithOne(g1 => g1.Movie)
                                        .HasForeignKey(o => o.MovieId)
                                        .OnDelete(DeleteBehavior.NoAction);

            // Movie(M) - MovieSession(1)
            modelBuilder.Entity<Movie>().HasMany(m => m.MovieSessions)
                                        .WithOne(m1 => m1.Movie)
                                        .HasForeignKey(m1 => m1.MovieId)
                                        .OnDelete(DeleteBehavior.NoAction);

            // Theater(M) - MovieSession(1)
            modelBuilder.Entity<Theather>().HasMany(m => m.MovieSessions)
                                        .WithOne(m1 => m1.Theather)
                                        .HasForeignKey(m1 => m1.TheatherId)
                                        .OnDelete(DeleteBehavior.NoAction);

            // MovieSession(1) - MovieSessionSeat(M)
            modelBuilder.Entity<MovieSession>().HasMany(m => m.MovieSessionSeats)
                                               .WithOne(m1 => m1.MovieSession)
                                               .HasForeignKey(m1 => m1.SessionId)
                                               .OnDelete(DeleteBehavior.NoAction);

            // TheatherSeat(1) - MovieSessionSeat(M)
            modelBuilder.Entity<TheatherSeat>().HasMany(m => m.MovieSessionSeats)
                                               .WithOne(m1 => m1.TheatherSeat)
                                               .HasForeignKey(m1 => m1.SeatId)
                                               .OnDelete(DeleteBehavior.NoAction);

            // Theather(1) - TheatherPrice(M)
            modelBuilder.Entity<Theather>().HasMany(m => m.TheatherPrices)
                                                .WithOne(m1 => m1.Theather)
                                                .HasForeignKey(m1 => m1.TheatherId)
                                                .OnDelete(DeleteBehavior.NoAction);

            // İlk veriler          

            modelBuilder.Entity<Customer>().HasData
                (
                    new Customer { Id = 1, Name = "İbrahim Ertan Yılmaz", Email = "ibrahim@gmail.com" },
                    new Customer { Id = 2, Name = "Orhan İnaç", Email = "inac.orhan@outlook.com" }
                );

            modelBuilder.Entity<OperationClaim>().HasData
                (
                new OperationClaim { Id = 1, Name = "admin" },
                new OperationClaim { Id = 2, Name = "user" }
                );

            modelBuilder.Entity<CustomerOperationClaim>().HasData
                (
                    new CustomerOperationClaim { Id = 1, CustomerId = 1, OperationClaimId = 1 }
                );
        }
    }
}
