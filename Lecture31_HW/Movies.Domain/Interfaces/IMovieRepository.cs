using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Movie movie);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Movie movie);
    }
}
