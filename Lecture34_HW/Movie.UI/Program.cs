using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie.Domain.DTOs;
using Movie.Domain.Entities;
using Movie.Domain.Interfaces;
using Movie.Infrastucture.Data;
using Movie.Infrastucture.Repositories;
using Movie.Service.Implementations;
using Movie.Service.Interfaces;

namespace Movie.UI
{
	internal class Program
	{
		static async Task Main(string[] args)
		{

			var servises = new ServiceCollection();

			servises.AddDbContext<MovieDbContext>();
			servises.AddScoped<IMovieRepository, MovieRepository>();
			servises.AddScoped<IMovieService, MovieService>();

			servises.AddScoped<IActorRepository, ActorRepository>();
			servises.AddScoped<IActorService, ActorService>();

			servises.AddScoped<IUnitOfWork, UnitOfWork>();

			var serviceProvider = servises.BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<MovieDbContext>();
            var movieService = serviceProvider.GetRequiredService<IMovieService>();
			var actorService = serviceProvider.GetRequiredService<IActorService>();


            await dbContext.Database.EnsureCreatedAsync();

            // კინოსტუდიების დამატება
            var warnerBros = await dbContext.Studios.FirstOrDefaultAsync(s => s.Name == "Warner Bros");
            if (warnerBros == null)
            {
                warnerBros = new Studio { Name = "Warner Bros", CountryId = 1 };
                dbContext.Studios.Add(warnerBros);
            }

            var universal = await dbContext.Studios.FirstOrDefaultAsync(s => s.Name == "Universal Pictures");
            if (universal == null)
            {
                universal = new Studio { Name = "Universal Pictures", CountryId = 1 };
                dbContext.Studios.Add(universal);
            }

            var paramount = await dbContext.Studios.FirstOrDefaultAsync(s => s.Name == "Paramount Pictures");
            if (paramount == null)
            {
                paramount = new Studio { Name = "Paramount Pictures", CountryId = 1 };
                dbContext.Studios.Add(paramount);
            }

            await dbContext.SaveChangesAsync();

            // კინოების დამატება
            var moviesToSeed = new List<(string Title, int Year, int StudioId)>
            {
                ("Home Alone 3", 1997, warnerBros.Id),
                ("Home Alone 4", 2002, warnerBros.Id),
                ("The Dark Knight", 2008, warnerBros.Id),
                ("Inception", 2010, warnerBros.Id),
                ("Harry Potter 1", 2001, warnerBros.Id),
                ("Oppenheimer", 2023, universal.Id),
                ("Jurassic Park", 1993, universal.Id),
                ("Interstellar", 2014, paramount.Id)
            };

            foreach (var item in moviesToSeed)
            {
                var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == item.Title);
                if (existingMovie == null)
                {
                    await movieService.AddMovieAsync(new CreateMovieDTO
                    {
                        Title = item.Title,
                        ReleaseYear = item.Year,
                        StudioId = item.StudioId
                    });
                }
            }

            // მსახიობების დამატება
            var actorsToSeed = new List<(string FirstName, string LastName)>
            {
                ("Macaulay", "Culkin"),
                ("Christian", "Bale"),
                ("Leonardo", "DiCaprio"),
                ("Cillian", "Murphy"),
                ("Daniel", "Radcliffe")
            };

            foreach (var item in actorsToSeed)
            {
                var existingActor = await dbContext.Actors.FirstOrDefaultAsync(a => a.FirstName == item.FirstName && a.LastName == item.LastName);
                if (existingActor == null)
                {
                    await actorService.AddActorAsync(new CreateActorDTO
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName
                    });
                }
            }

            // კინოს და მსახიობის კავშირი
            var darkKnight = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "The Dark Knight");
            var inception = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "Inception");
            var oppenheimer = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "Oppenheimer");
            var homeAlone3 = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "Home Alone 3");
            var harryPotter = await dbContext.Movies.FirstOrDefaultAsync(m => m.Title == "Harry Potter 1");

            // Christian Bale -> The Dark Knight
            var bale = await dbContext.Actors.FirstOrDefaultAsync(a => a.LastName == "Bale");
            if (bale != null && darkKnight != null)
            {
                await actorService.UpdateActorMoviesAsync(bale.Id, new UpdateActorMovieDTO { MovieIds = new List<int> { darkKnight.Id } });
            }

            // Cillian Murphy -> The Dark Knight, Oppenheimer, Inception
            var murphy = await dbContext.Actors.FirstOrDefaultAsync(a => a.LastName == "Murphy");
            if (murphy != null && darkKnight != null && oppenheimer != null && inception != null)
            {
                await actorService.UpdateActorMoviesAsync(murphy.Id, new UpdateActorMovieDTO { MovieIds = new List<int> { darkKnight.Id, oppenheimer.Id, inception.Id } });
            }

            // Leonardo DiCaprio -> Inception
            var dicaprio = await dbContext.Actors.FirstOrDefaultAsync(a => a.LastName == "DiCaprio");
            if (dicaprio != null && inception != null)
            {
                await actorService.UpdateActorMoviesAsync(dicaprio.Id, new UpdateActorMovieDTO { MovieIds = new List<int> { inception.Id } });
            }

            // Daniel Radcliffe -> Harry Potter 1
            var radcliffe = await dbContext.Actors.FirstOrDefaultAsync(a => a.LastName == "Radcliffe");
            if (radcliffe != null && harryPotter != null)
            {
                await actorService.UpdateActorMoviesAsync(radcliffe.Id, new UpdateActorMovieDTO { MovieIds = new List<int> { harryPotter.Id } });
            }

            Console.WriteLine("\n--- Actors and Movies ---");
            var actorsWithMovies = await dbContext.Actors
                .Include(a => a.Movies)
                .ToListAsync();

            foreach (var item in actorsWithMovies)
            {
                Console.Write($"{item.FirstName} {item.LastName}");
                foreach (var movie in item.Movies)
                {
                    Console.Write($" - {movie.Title}");
                }
                Console.WriteLine();
            }

            CancellationTokenSource cts = new CancellationTokenSource();
			var movvies = await movieService.GetAllMoviesAsync(cts.Token);


			var searchedFilms = await movieService.SearchMoviesByStudioAsync(1990, "Warner Bros", 1);


			foreach (var item in searchedFilms)
			{
				Console.WriteLine(item);
			}

		}
	}
}
