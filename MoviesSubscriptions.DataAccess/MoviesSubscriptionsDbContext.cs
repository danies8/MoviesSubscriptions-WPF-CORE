using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MoviesSubscriptions.DataAccess
{

    // Add-Migration XXX
    // Update-Database
    public class MoviesSubscriptionsDbContext : DbContext
    {
        public MoviesSubscriptionsDbContext() :
            base(GetOptions("Data Source=DESKTOP-AMH1I7B\\SQLEXPRESS01;Initial Catalog=MoviesSubscriptions;Integrated Security=True"))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Movie> Movies { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
           .HasIndex(u => u.UserName)
            .IsUnique();


            // admin
            modelBuilder.Entity<UserLogin>().HasData(
                new UserLogin() { Id = 1, UserName = "admin@yahoo.com", Password = "1234", IsAdmin = true });

            modelBuilder.Entity<User>().HasData(
               new User()
               {
                   Id = 1,
                   FirstName = "Dani",
                   LastName = "Shitrit",
                   UserName = "admin@yahoo.com",
                   CreatedDate = new DateTime(2020, 11, 02),
                   SessionTimeOut = 9999,
                   ViewSubscriptions = true,
                   CreateSubscriptions = true,
                   UpdateSubscriptions = true,
                   DeleteSubscriptions = true,
                   ViewMovies = true,
                   CreateMovies = true,
                   UpdateMovies = true,
                   DeleteMovies = true
               });


            // non admin
            modelBuilder.Entity<UserLogin>().HasData(
               new UserLogin() { Id = 2, UserName = "nonAdmin@yahoo.com", Password = "1234", IsAdmin = false });

            modelBuilder.Entity<User>().HasData(
              new User()
              {
                  Id = 2,
                  FirstName = "Yael",
                  LastName = "Shitrit",
                  UserName = "nonAdmin@yahoo.com",
                  CreatedDate = new DateTime(2020, 11, 02),
                  SessionTimeOut = 2,
                  ViewSubscriptions = true,
                  CreateSubscriptions = true,
                  UpdateSubscriptions = true,
                  DeleteSubscriptions = true,
                  ViewMovies = true,
                  CreateMovies = true,
                  UpdateMovies = true,
                  DeleteMovies = true
              });


            // Mebers
            modelBuilder.Entity<Member>().HasData(
                new Member() { Id = 1, Name = "Lihi Shitrit", Email = "lihi@yahoo.com", City = "Nahariya" });
            modelBuilder.Entity<Member>().HasData(
               new Member() { Id = 2, Name = "Gili Shitrit", Email = "gili@yahoo.com", City = "Nahariya" });
            modelBuilder.Entity<Member>().HasData(
               new Member() { Id = 3, Name = "Omri Shitrit", Email = "omri@yahoo.com", City = "Nahariya" });
            modelBuilder.Entity<Member>().HasData(
               new Member() { Id = 4, Name = "Shir Shitrit", Email = "shir@yahoo.com", City = "Nahariya" });

            // Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    Name = "The Affair",
                    Genres = "Drama, Romance",
                    ImageUrl = "http://static.tvmaze.com/uploads/images/original_untouched/203/509941.jpg",
                    Premired = new DateTime(2014, 10, 12),
                });
            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 2,
                    Name = "Scandal",
                    Genres = "Drama, Thriller",
                    ImageUrl = "http://static.tvmaze.com/uploads/images/original_untouched/124/310268.jpg",
                    Premired = new DateTime(2014, 4, 5),
                 });



            // Subscription
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration#:~:text=Configuring%20Many%20To%20Many%20Relationships%20in%20Entity%20Framework,categories%20and%20a%20category%20can%20contain%20many%20books.
           modelBuilder.Entity<Subscription>()
           .HasData(
               new Subscription()
               {
                   Id = 1,
                   MemberId = 1,
                   MovieId = 1,
                   WatchedDate = new DateTime(2014, 4, 5),
               }); ;

            modelBuilder.Entity<Subscription>()
            .HasData(
                new Subscription()
                {
                    Id = 2,
                    MemberId = 1,
                    MovieId = 2,
                    WatchedDate = new DateTime(2014, 5, 5),
                });
        }

    }
}
