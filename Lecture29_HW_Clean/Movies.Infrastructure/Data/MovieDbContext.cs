using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Data
{
    public class MovieDbContext : DbContext
    {
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<StudioDetails> StudioDetails { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connection string-ს ვკითხულობთ appsettings.json ფაილიდან
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Country
            modelBuilder.Entity<Country>().HasKey(c => c.Id);

            modelBuilder.Entity<Country>().Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Country - Studio (1:M)
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Studios)
                .WithOne(s => s.Country)
                .HasForeignKey(s => s.CountryId);

            // Studio
            modelBuilder.Entity<Studio>().HasKey(s => s.Id);

            modelBuilder.Entity<Studio>().Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Studio - StudioDetails (1:1)
            modelBuilder.Entity<Studio>()
                .HasOne(s => s.StudioDetails)
                .WithOne(sd => sd.Studio)
                .HasForeignKey<StudioDetails>(sd => sd.StudioId);

            // Studio - Movie (1:M)
            modelBuilder.Entity<Studio>()
                .HasMany(s => s.Movies)
                .WithOne(m => m.Studio)
                .HasForeignKey(m => m.StudioId);

            // StudioDetails
            modelBuilder.Entity<StudioDetails>().HasKey(sd => sd.Id);

            modelBuilder.Entity<StudioDetails>().Property(sd => sd.LicenseNumber)
                .IsRequired();

            // Movie
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);

            modelBuilder.Entity<Movie>().Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(150);

            // Movie - Actor (M:N)
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(j => j.ToTable("MovieActors"));

            // Actor
            modelBuilder.Entity<Actor>().HasKey(a => a.Id);

            modelBuilder.Entity<Actor>().Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Actor>().Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            //seeding
            modelBuilder.Entity<Country>()
                .HasData(
                    new Country { Id = 1, Name = "USA" },
                    new Country { Id = 2, Name = "UK" },
                    new Country { Id = 3, Name = "France" }
                );
        }

    }
}
