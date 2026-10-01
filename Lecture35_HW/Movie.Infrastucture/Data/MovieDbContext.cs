using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastucture.Data
{
	public class MovieDbContext : DbContext
	{
		public DbSet<Movie.Domain.Entities.Movie> Movies { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Studio> Studios { get; set; }
		public DbSet<StudioDetails> StudioDetails { get; set; }


		public MovieDbContext()
		{

		}

		public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				var configuration = new ConfigurationBuilder()
					.SetBasePath(AppContext.BaseDirectory)
					.AddJsonFile("appsettings.json")
					.Build();
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				optionsBuilder.UseSqlServer(connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Movie.Domain.Entities.Movie>()
				.HasMany(m => m.Actors)
				.WithMany(a => a.Movies);

			modelBuilder.Entity<Movie.Domain.Entities.Movie>().Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(100);


			// studioDetails დავამატე მაგრამ არაა აუცილებელი
			modelBuilder.Entity<StudioDetails>()
			.Property(sd => sd.LicenseNumber)
			.IsRequired()
			.HasMaxLength(100);

			modelBuilder.Entity<Studio>()
				.HasOne(s => s.StudioDetails)
				.WithOne(sd => sd.Studio)
				.HasForeignKey<StudioDetails>(sd => sd.StudioId)
				.OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<Studio>().Property(m => m.Name)
					.IsRequired()
					.HasMaxLength(100);


			modelBuilder.Entity<Studio>()
				.HasMany(s => s.Movies)
				.WithOne(m => m.Studio)
				.HasForeignKey(m => m.StudioId);



			modelBuilder.Entity<Country>()
				.HasMany(c => c.Studios)
				.WithOne(s => s.Country)
				.HasForeignKey(s => s.CountryId)
				.OnDelete(DeleteBehavior.Restrict);

			//ეს ზედმეტია ვაკომენტარებ ზემოთ წერია 

			//modelBuilder.Entity<Actor>()
			//	.HasMany(a => a.Movies)
			//	.WithMany(m => m.Actors);


			//data seeding
			modelBuilder.Entity<Country>()
				.HasData(
					new Country { Id = 1, Name = "USA" },
					new Country { Id = 2, Name = "UK" },
					new Country { Id = 3, Name = "France" }
				);

		}
	}
}
