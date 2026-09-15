using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Domain.Interfaces;
using Movies.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieDbContext _movieDbContext;
        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _movieDbContext.Movies.AddAsync(movie);
            await _movieDbContext.SaveChangesAsync();
        }
        public async Task<ICollection<Movie>> GetAllMoviesAsync()
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _movieDbContext.Movies
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // დავალება
        public async Task UpdateMovieAsync(Movie movie)
        {
            _movieDbContext.Movies.Update(movie);
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(Movie movie)
        {
            _movieDbContext.Movies.Remove(movie);
            await _movieDbContext.SaveChangesAsync();
        }

    }
}
