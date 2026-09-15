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
    }
}
