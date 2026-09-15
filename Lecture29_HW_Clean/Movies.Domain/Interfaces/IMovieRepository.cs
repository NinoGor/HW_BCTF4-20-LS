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
    }
}
