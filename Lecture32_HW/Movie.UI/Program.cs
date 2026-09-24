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
			#region without DI container

			var dbContext = new MovieDbContext();
			//var movieRepository = new MovieRepository(dbContext);
			//var movieService = new MovieService(movieRepository);
			#endregion

			//DI container

			var servises = new ServiceCollection();

			servises.AddDbContext<MovieDbContext>();
			servises.AddScoped<IMovieRepository, MovieRepository>();
			servises.AddScoped<IMovieService, MovieService>();

			servises.AddScoped<IActorRepository, ActorRepository>();
			servises.AddScoped<IActorService, ActorService>();


			servises.AddScoped<IUnitOfWork, UnitOfWork>();




			var serviceProvider = servises.BuildServiceProvider();

			var movieService = serviceProvider.GetRequiredService<IMovieService>();
			var actorService = serviceProvider.GetRequiredService<IActorService>();


			#region old code

			//var studio = new Studio { Name = "Warner Bros", CountryId = 1 };
			//dbContext.Studios.Add(studio);
			//await dbContext.SaveChangesAsync();



			//var createMovieDto = new CreateMovieDTO { Title = "Home Alone", ReleaseYear = 1996, StudioId = 1 };
			//await movieService.AddMovieAsync(createMovieDto);
			//await dbContext.SaveChangesAsync();


			//var movieById = await movieService.GetMovieByIdAsync(1);
			//Console.WriteLine(movieById);




			//var movies = await movieService.GetAllMoviesAsync();
			//foreach (var movie in movies)
			//{
			//	Console.WriteLine(movie);
			//}
			#endregion 




			//var studio = new Studio { Name = "Warner Bros", CountryId = 1 };
			//dbContext.Studios.Add(studio);
			//await dbContext.SaveChangesAsync();



			//var st = await dbContext.Studios
			//	 .FirstOrDefaultAsync(s => s.Name == "Warner Bros");

			//var movieDto = new CreateMovieDTO 
			//{ Title = "Home Alone 3", ReleaseYear = 1996, StudioId = st.Id };


			//var movie2Dto = new CreateMovieDTO
			//{ Title = "Home Alone 4", ReleaseYear = 1998, StudioId = st.Id };



			//await movieService.AddMovieAsync(movieDto);
			//await movieService.AddMovieAsync(movie2Dto);




			//var actorDTO = new CreateActorDTO {
			//	FirstName = "Macaulay", 
			//	LastName = "Culkin",
			//	};


			//await actorService.AddActorAsync(actorDTO);







			//var film1 = await dbContext.Movies
			//	.FirstAsync(m => m.Title == "Home Alone 3");
			//var film2 = await dbContext.Movies
			//.FirstAsync(m => m.Title == "Home Alone 4");



			//var updateActorDTO = new UpdateActorMovieDTO
			//{
			//	MovieIds = new List<int> { film1.Id, film2.Id }
			//};



			//await actorService.UpdateActorMoviesAsync(1, updateActorDTO);





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

		}
	}
}
