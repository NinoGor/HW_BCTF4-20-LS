using Movies.Domain.DTOs;
using Movies.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Service.Interfaces
{
    public interface IMovieService
    {
        Task<ICollection<MovieDTO>> GetAllMoviesAsync();
        Task AddMovieAsync(CreateMovieDTO createMovieDTO);
        Task<MovieDTO?> GetMovieByIdAsync(int id);
        Task UpdateMovieAsync(int id, UpdateMovieDTO updateMovieDTO);
        Task DeleteMovieAsync(int id);
    }
}
