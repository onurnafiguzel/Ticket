﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ticket.Data;

#nullable disable

namespace Ticket.Data.Migrations
{
    [DbContext(typeof(TicketContext))]
    partial class TicketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "ibrahim@gmail.com",
                            Name = "İbrahim Ertan Yılmaz",
                            Status = false
                        },
                        new
                        {
                            Id = 2,
                            Email = "inac.orhan@outlook.com",
                            Name = "Orhan İnaç",
                            Status = false
                        });
                });

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.CustomerOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("OperationClaimId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OperationClaimId");

                    b.ToTable("CustomerOperationClaims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            OperationClaimId = 1
                        });
                });

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        },
                        new
                        {
                            Id = 3,
                            Name = "god"
                        });
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PlaceOfBirth")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProfilePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Cast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.ToTable("Casts");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BackdropPath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("NowPlaying")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OriginalLanguage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OriginalTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PosterPath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Rating")
                        .HasColumnType("double");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Slug")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TrailerUrl")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("MovieId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("TheatherId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheatherId");

                    b.ToTable("MovieSessions");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieSessionSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SeatId");

                    b.HasIndex("SessionId");

                    b.ToTable("MovieSessionSeats");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Theather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<string>("SeatPlan")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Theathers");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.TheatherPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TheatherId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheatherId");

                    b.ToTable("TheatherPrices");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.TheatherSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TheatherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TheatherSeats");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Seats")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.CustomerOperationClaim", b =>
                {
                    b.HasOne("Ticket.Application.Entities.Concrete.Customer", "Customer")
                        .WithMany("OperationClaims")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ticket.Application.Entities.Concrete.OperationClaim", "OperationClaim")
                        .WithMany("Customers")
                        .HasForeignKey("OperationClaimId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("OperationClaim");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Cast", b =>
                {
                    b.HasOne("Ticket.Domain.Entities.Concrete.Actor", "Actor")
                        .WithMany("Casts")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Actor");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieGenre", b =>
                {
                    b.HasOne("Ticket.Domain.Entities.Concrete.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId");

                    b.HasOne("Ticket.Domain.Entities.Concrete.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieSession", b =>
                {
                    b.HasOne("Ticket.Domain.Entities.Concrete.Movie", "Movie")
                        .WithMany("MovieSessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ticket.Domain.Entities.Concrete.Theather", "Theather")
                        .WithMany("MovieSessions")
                        .HasForeignKey("TheatherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Theather");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieSessionSeat", b =>
                {
                    b.HasOne("Ticket.Application.Entities.Concrete.Customer", null)
                        .WithMany("MovieSessionSeats")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Ticket.Domain.Entities.Concrete.TheatherSeat", "TheatherSeat")
                        .WithMany("MovieSessionSeats")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ticket.Domain.Entities.Concrete.MovieSession", "MovieSession")
                        .WithMany("MovieSessionSeats")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MovieSession");

                    b.Navigation("TheatherSeat");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Theather", b =>
                {
                    b.HasOne("Ticket.Domain.Entities.Concrete.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.TheatherPrice", b =>
                {
                    b.HasOne("Ticket.Domain.Entities.Concrete.Theather", "Theather")
                        .WithMany("TheatherPrices")
                        .HasForeignKey("TheatherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Theather");
                });

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.Customer", b =>
                {
                    b.Navigation("MovieSessionSeats");

                    b.Navigation("OperationClaims");
                });

            modelBuilder.Entity("Ticket.Application.Entities.Concrete.OperationClaim", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Actor", b =>
                {
                    b.Navigation("Casts");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Movie", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("MovieSessions");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.MovieSession", b =>
                {
                    b.Navigation("MovieSessionSeats");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.Theather", b =>
                {
                    b.Navigation("MovieSessions");

                    b.Navigation("TheatherPrices");
                });

            modelBuilder.Entity("Ticket.Domain.Entities.Concrete.TheatherSeat", b =>
                {
                    b.Navigation("MovieSessionSeats");
                });
#pragma warning restore 612, 618
        }
    }
}
