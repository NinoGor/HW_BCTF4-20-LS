using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;

        public EditModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public UpdateMovieDTO Movie { get; set; } = new();

        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);

                Id = id;
                Movie = new UpdateMovieDTO
                {
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear,
                    StudioId = movie.StudioId
                };

                return Page();
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Movies/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Id = id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _movieService.UpdateMovieAsync(id, Movie);
                TempData["Success"] = "Movie updated successfully.";
                return RedirectToPage("/Movies/Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                // ვალიდაციის ერორის შემთხვევაში, ვაჩვენებთ იგივე გვერდს შეცდომის შეტყობინებით
                return Page();
            }
        }
    }
}