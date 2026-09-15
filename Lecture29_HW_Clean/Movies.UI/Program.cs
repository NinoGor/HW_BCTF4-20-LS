using Movies.Domain.DTOs;
using Movies.Domain.Entities;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Service.Implementations;

namespace Movies.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var dbContext = new MovieDbContext();
            var movieRepository = new MovieRepository(dbContext);
            var movieService = new MovieService(movieRepository);


            //var studio = new Studio
            //{
            //    Name = "Warner Bros.",
            //    CountryId = 1
            //};
            //dbContext.Studios.Add(studio);
            //await dbContext.SaveChangesAsync();

            //await movieService.AddMovieAsync(new CreateMovieDTO
            //{
            //    Title = "Home Alone",
            //    ReleaseYear = 1990,
            //    StudioId = 1
            //});
            //await dbContext.SaveChangesAsync();

            var movies = await movieService.GetAllMoviesAsync();
            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
    }
}
