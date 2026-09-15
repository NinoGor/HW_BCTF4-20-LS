using Microsoft.Extensions.DependencyInjection;
using Movies.Domain.DTOs;
using Movies.Domain.Entities;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Service.Implementations;
using Movies.Service.Interfaces;

namespace Movies.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Without DI Container
            //var dbContext = new MovieDbContext();
            //var movieRepository = new MovieRepository(dbContext);
            //var movieService = new MovieService(movieRepository);
            #endregion

            var services = new ServiceCollection();

            services.AddDbContext<MovieDbContext>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            var serviceProvider = services.BuildServiceProvider();
            var movieService = serviceProvider.GetRequiredService<IMovieService>();


            #region Add Studio
            // ADD STUDIO
            //var studio = new Studio
            //{
            //    Name = "Warner Bros.",
            //    CountryId = 1
            //};
            //dbContext.Studios.Add(studio);
            //await dbContext.SaveChangesAsync();
            #endregion

            #region Add Movie
            //await movieService.AddMovieAsync(new CreateMovieDTO
            //{
            //    Title = "The Matrix",
            //    ReleaseYear = 1999,
            //    StudioId = 1
            //});

            //await movieService.AddMovieAsync(new CreateMovieDTO
            //{
            //    Title = "Home Alone",
            //    ReleaseYear = 1990,
            //    StudioId = 1
            //});
            // save ხდება AddMovieAsync მეთოდში
            #endregion

            #region Get Movie By ID
            //int targetId = 1;
            //var movie = await movieService.GetMovieByIdAsync(targetId);

            //if (movie != null)
            //{
            //    Console.WriteLine($"Found: {movie}");
            //}
            //else
            //{
            //    Console.WriteLine($"Movie with ID={targetId} was not found.");
            //}
            #endregion

            // Update  |  ID=1 მქონე ფილმის
            try
            {
                var updateDto = new UpdateMovieDTO
                {
                    Title = "The Shawshank Redemption",
                    ReleaseYear = 1994,
                    StudioId = 1
                };

                await movieService.UpdateMovieAsync(1, updateDto);
                Console.WriteLine("Movie updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
            }
            await DisplayAllMoviesAsync(movieService);


            // Delete  |  ID=2 მქონე ფილმის წაშლა
            try
            {
                await movieService.DeleteMovieAsync(2);
                Console.WriteLine("Movie deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete failed: {ex.Message}");
            }

            await DisplayAllMoviesAsync(movieService);
        }

        // helper მეთოდი, რომელიც დაბეჭდავს ყველა ფილმს 
        private static async Task DisplayAllMoviesAsync(IMovieService movieService)
        {
            Console.WriteLine("\n------- MOVIE LIST -------");
            var movies = await movieService.GetAllMoviesAsync();

            if (!movies.Any())
            {
                Console.WriteLine("No movies found.");
                Console.WriteLine("--------------------------\n");
                return;
            }

            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine("--------------------------\n");
        }
    }
}
