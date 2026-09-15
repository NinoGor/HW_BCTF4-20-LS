using Movies.Domain.DTOs;
using Movies.Domain.Entities;
using Movies.Domain.Interfaces;
using Movies.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository) 
        { 
            _movieRepository = movieRepository;
        }

        public async Task<ICollection<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            var movieDTOs = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseYear = m.ReleaseYear,
                StudioName = m.Studio.Name
            }).ToList();
            
            return movieDTOs;
        }

        public async Task AddMovieAsync(CreateMovieDTO createMovieDTO)
        {
            if(createMovieDTO == null)
            {
                throw new ArgumentNullException(nameof(createMovieDTO));
            }
            if(string.IsNullOrEmpty(createMovieDTO.Title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(createMovieDTO.Title));
            }
            if(createMovieDTO.ReleaseYear < 1888) // პირველი ფილმი გამოვიდა 1888-ში
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.ReleaseYear), "Release year must be 1888 or later");
            }
            if(createMovieDTO.ReleaseYear > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.ReleaseYear), "Release year cannot be in the future");
            }
            if(createMovieDTO.StudioId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(createMovieDTO.StudioId), "Studio ID must be a positive integer");
            }
            var movie = new Movie
            {
                Title = createMovieDTO.Title,
                ReleaseYear = createMovieDTO.ReleaseYear,
                StudioId = createMovieDTO.StudioId
            };
            await _movieRepository.AddMovieAsync(movie);
        }

        public async Task<MovieDTO?> GetMovieByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a positive integer.");
            }

            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                StudioName = movie.Studio?.Name ?? "Unknown"
            };
        }

        // დავალება
        public async Task UpdateMovieAsync(int id, UpdateMovieDTO updateMovieDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a positive integer.");
            }
            if (updateMovieDTO == null)
            {
                throw new ArgumentNullException(nameof(updateMovieDTO));
            }
            if (string.IsNullOrEmpty(updateMovieDTO.Title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(updateMovieDTO.Title));
            }
            if (updateMovieDTO.ReleaseYear < 1888) // პირველი ფილმი გამოვიდა 1888-ში
            {
                throw new ArgumentOutOfRangeException(nameof(updateMovieDTO.ReleaseYear), "Release year must be 1888 or later");
            }
            if (updateMovieDTO.ReleaseYear > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(updateMovieDTO.ReleaseYear), "Release year cannot be in the future");
            }
            if (updateMovieDTO.StudioId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(updateMovieDTO.StudioId), "Studio ID must be a positive integer");
            }

            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} was not found.");
            }

            existingMovie.Title = updateMovieDTO.Title;
            existingMovie.ReleaseYear = updateMovieDTO.ReleaseYear;
            existingMovie.StudioId = updateMovieDTO.StudioId;

            await _movieRepository.UpdateMovieAsync(existingMovie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be a positive integer.");
            }

            var movieToDelete = await _movieRepository.GetMovieByIdAsync(id); // დააბრუნებს null-ს თუ ამ ID-ით ფილმი არ გვაქვს
            if (movieToDelete == null)
            {
                // თუ ფილმი ვერ ვიპოვეთ, გავისროლოთ KeyNotFoundException
                throw new KeyNotFoundException($"Movie with ID {id} was not found.");
            }

            await _movieRepository.DeleteMovieAsync(movieToDelete);
        }
    }
}
