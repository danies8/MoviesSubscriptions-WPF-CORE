﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesSubscriptions.DataAccess;

namespace MoviesSubscriptions.DataAccess.Migrations
{
    [DbContext(typeof(MoviesSubscriptionsDbContext))]
    [Migration("20201119071957_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoviesSubscriptions.Model.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Nahariya",
                            Email = "lihi@yahoo.com",
                            Name = "Lihi Shitrit"
                        },
                        new
                        {
                            Id = 2,
                            City = "Nahariya",
                            Email = "gili@yahoo.com",
                            Name = "Gili Shitrit"
                        },
                        new
                        {
                            Id = 3,
                            City = "Nahariya",
                            Email = "omri@yahoo.com",
                            Name = "Omri Shitrit"
                        },
                        new
                        {
                            Id = 4,
                            City = "Nahariya",
                            Email = "shir@yahoo.com",
                            Name = "Shir Shitrit"
                        });
                });

            modelBuilder.Entity("MoviesSubscriptions.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Premired")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genres = "Drama, Romance",
                            ImageUrl = "http://static.tvmaze.com/uploads/images/original_untouched/203/509941.jpg",
                            Name = "The Affair",
                            Premired = new DateTime(2014, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Genres = "Drama, Thriller",
                            ImageUrl = "http://static.tvmaze.com/uploads/images/original_untouched/124/310268.jpg",
                            Name = "Scandal",
                            Premired = new DateTime(2014, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MoviesSubscriptions.Model.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("MovieId");

                    b.ToTable("Subscription");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MemberId = 1,
                            MovieId = 1,
                            WatchedDate = new DateTime(2014, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            MemberId = 1,
                            MovieId = 2,
                            WatchedDate = new DateTime(2014, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MoviesSubscriptions.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CreateMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("CreateSubscriptions")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeleteMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("DeleteSubscriptions")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("SessionTimeOut")
                        .HasColumnType("int");

                    b.Property<bool>("UpdateMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("UpdateSubscriptions")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("ViewMovies")
                        .HasColumnType("bit");

                    b.Property<bool>("ViewSubscriptions")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateMovies = true,
                            CreateSubscriptions = true,
                            CreatedDate = new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeleteMovies = true,
                            DeleteSubscriptions = true,
                            FirstName = "Dani",
                            LastName = "Shitrit",
                            SessionTimeOut = 9999,
                            UpdateMovies = true,
                            UpdateSubscriptions = true,
                            UserName = "admin@yahoo.com",
                            ViewMovies = true,
                            ViewSubscriptions = true
                        },
                        new
                        {
                            Id = 2,
                            CreateMovies = true,
                            CreateSubscriptions = true,
                            CreatedDate = new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeleteMovies = true,
                            DeleteSubscriptions = true,
                            FirstName = "Yael",
                            LastName = "Shitrit",
                            SessionTimeOut = 2,
                            UpdateMovies = true,
                            UpdateSubscriptions = true,
                            UserName = "nonAdmin@yahoo.com",
                            ViewMovies = true,
                            ViewSubscriptions = true
                        });
                });

            modelBuilder.Entity("MoviesSubscriptions.Model.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserLogins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAdmin = true,
                            Password = "1234",
                            UserName = "admin@yahoo.com"
                        },
                        new
                        {
                            Id = 2,
                            IsAdmin = false,
                            Password = "1234",
                            UserName = "nonAdmin@yahoo.com"
                        });
                });

            modelBuilder.Entity("MoviesSubscriptions.Model.Subscription", b =>
                {
                    b.HasOne("MoviesSubscriptions.Model.Member", "Member")
                        .WithMany("Subscriptions")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesSubscriptions.Model.Movie", "Movie")
                        .WithMany("Subscriptions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
