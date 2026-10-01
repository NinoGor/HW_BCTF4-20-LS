using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie.Domain.DTOs;
using Movie.Domain.Interfaces;
using Movie.Service.Interfaces;

namespace Movie.Web1.Pages.Movies
{
    public class IndexModel : PageModel
    {
		//public  List<string> Movies = new List<string>();



		private readonly IMovieService _movieService;

		public ICollection<MovieDTO> Movies { get; set; } = new List<MovieDTO>();

		public IndexModel(IMovieService movieService)
		{
			_movieService = movieService;
		}



		[BindProperty]
		public int Year { get; set; }

		[BindProperty]
		public string StudioName { get; set; }

		[BindProperty]
		public int MinimumActorCount { get; set; }



		public async Task OnPostSearchAsync()
		{
			try
			{
				var searchResults = await _movieService.SearchMoviesByStudioAsync(
				Year, StudioName, MinimumActorCount);


				Movies = searchResults

					 .Select(x => new MovieDTO
					 {
						 Title = x.Title,

						 StudioName = x.StudioName,

						 ReleaseYear = x.ReleaseYear
					 }).ToList();

			}

			catch (ArgumentException ex)
			{
				TempData["Error"] = ex.Message;
				// await OnGetAsync();
			}





		}




		public async Task OnGetAsync()
		{
			Movies = await _movieService.GetAllMoviesAsync();
		}



		//public async Task OnPostDeleteAsync(int id)
		//{
		//	try
		//	{
		//		await _movieService.DeleteMovieAsync(id);
		//		TempData["Success"] = "Movie deleted successfully.";
		//	}catch (ArgumentException ex)
		//	{
		//		TempData["Error"] = ex.Message;
		//	}

		//	await OnGetAsync();

		//}



		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			try
			{
				await _movieService.DeleteMovieAsync(id);
				TempData["Success"] = "Movie deleted successfully.";
			}
			catch (ArgumentException ex)
			{
				TempData["Error"] = ex.Message;
			}


			return RedirectToPage();


		}




		//public void OnGet()
		//      {
		//	Movies.Add("The Matrix");
		//	Movies.Add("The Matrix Reloaded");
		//	Movies.Add("The Matrix Revolutions");
		//      }
	}
}
