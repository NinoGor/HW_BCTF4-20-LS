using Microsoft.EntityFrameworkCore;
using Movie.Domain.Interfaces;
using Movie.Infrastucture.Data;

namespace Movie.Infrastucture.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        #region გაკვეთილზე გაკეთებული
        private readonly MovieDbContext _movieDbContext;
        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }


        public async Task AddMovieAsync(Domain.Entities.Movie movie)
        {
            await _movieDbContext.Movies.AddAsync(movie);
            //await _movieDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Domain.Entities.Movie>> GetAllMoviesAsync()
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                .ToListAsync();
        }


        public async Task<Domain.Entities.Movie> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task UpdateMovieAsync(int id, Domain.Entities.Movie movie)
        {
            var movieExists =
                await _movieDbContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieExists == null)
            {
                throw new ArgumentException("Movie not found");
            }

            movieExists.Title = movie.Title;
            movieExists.ReleaseYear = movie.ReleaseYear;
            movieExists.StudioId = movie.StudioId;


            //await _movieDbContext.SaveChangesAsync();
        }


        public async Task DeleteMovieAsync(int id)
        {

            var movieExists = await _movieDbContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieExists == null)
            {
                throw new ArgumentException("Movie not found");
            }

            _movieDbContext.Movies.Remove(movieExists);
            //await _movieDbContext.SaveChangesAsync();

        }
        #endregion

        // ----------------------------------------------------------
        // ----------------------  დავალებები  ----------------------
        // ----------------------------------------------------------

        // გითჰაბიდან წამოვიღე Movie პროექტები, რაც გაკვეთილზე გავაკეთეთ, რომ შესაბამისად იყოს დავალება
        // მეთდებს დავამატებ ასევე IMovieRepository-ში, დანარჩენს უცვლელად ავტვირთავ


        // დავალება 1 - Where, OrderByDescending, ThenBy
        public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByStudioAsync(
            int year,
            string studioName,
            int minimumActorCount)
        {
            return await _movieDbContext.Movies
                /* დაკავშირებული ობიექტები - სამივე დავალებაში დავამატე, თუმცა აუცილებელი არაა.                 
                   დაგვჭირდება თუ გვინდა დაბრუნებულ Movie ობიექტებს C#-ში Studio და Actors სრულად მოჰყვეთ */
                .Include(m => m.Studio)
                .Include(m => m.Actors)
                .Where(m => m.ReleaseYear >= year // 1. ReleaseYear უნდა იყოს year-ზე მეტი ან ტოლი
                         && m.Studio.Name == studioName //2. Studio.Name უნდა უდრიდეს studioName-ს
                         && m.Actors.Count >= minimumActorCount) // 3. Actors.Count უნდა იყოს minimumActorCount-ზე მეტი ან ტოლი
                .OrderByDescending(m => m.ReleaseYear) // შედეგები დაალაგეთ პირველ რიგში ReleaseYear-ის მიხედვით კლებადობით
                .ThenBy(m => m.Title) // შემდეგ Title-ის მიხედვით ზრდადობით
                .ToListAsync();
        }

        // დავალება 2 - Where, OrderBy, ThenByDescending, ThenBy
        public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesByCountryAsync(
            string countryName,
            int minimumYear,
            int maximumActorCount)
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                    .ThenInclude(s => s.Country) // თუ გვინდა Country ობიექტი არ იყოს null, მაგ. movie.Studio.Country.Name-ის გამოყენება გვინდა
                .Include(m => m.Actors)
                .Where(m => m.Studio.Country.Name == countryName // 1. Studio.Country.Name უნდა უდრიდეს countryName-ს
                         && m.ReleaseYear >= minimumYear // 2. ReleaseYear უნდა იყოს minimumYear-ზე მეტი ან ტოლი
                         && m.Actors.Count <= maximumActorCount) // 3. Actors.Count უნდა იყოს maximumActorCount-ზე ნაკლები ან ტოლი
                .OrderBy(m => m.Actors.Count) // შედეგები დაალაგეთ პირველ რიგში მსახიობების რაოდენობის მიხედვით ზრდადობით
                .ThenByDescending(m => m.ReleaseYear) // შემდეგ ReleaseYear-ის მიხედვით კლებადობით
                .ThenBy(m => m.Title) // შემდეგ Title-ის მიხედვით ზრდადობით
                .ToListAsync();
        }

        // დავალება 3 - Where, Contains, OrderByDescending, ThenByDescending, ThenBy
        public async Task<ICollection<Domain.Entities.Movie>> SearchMoviesAdvancedAsync(
            int fromYear,
            int toYear,
            string countryName,
            string titleText,
            int minimumActorCount)
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                    .ThenInclude(s => s.Country)
                .Include(m => m.Actors)
                .Where( // 1. ReleaseYear უნდა იყოს fromYear - სა და toYear - ს შორის (ორივე ჩათვლით)
                         m => m.ReleaseYear >= fromYear && m.ReleaseYear <= toYear
                         && m.Studio.Country.Name == countryName // 2. Studio.Country.Name უნდა უდრიდეს countryName-ს
                         && m.Title.Contains(titleText) // 3. Title უნდა შეიცავდეს titleText-ს
                         && m.Actors.Count >= minimumActorCount) // 4. Actors.Count უნდა იყოს minimumActorCount-ზე მეტი ან ტოლი
                .OrderByDescending(m => m.Actors.Count) // შედეგები დაალაგეთ პირველ რიგში მსახიობების რაოდენობის მიხედვით კლებადობით
                .ThenByDescending(m => m.ReleaseYear) // შემდეგ ReleaseYear-ის მიხედვით კლებადობით
                .ThenBy(m => m.Studio.Name) // შემდეგ Studio.Name-ის მიხედვით ზრდადობით
                .ThenBy(m => m.Title) // შემდეგ Title-ის მიხედვით ზრდადობით
                .ToListAsync();
        }

    }
}
