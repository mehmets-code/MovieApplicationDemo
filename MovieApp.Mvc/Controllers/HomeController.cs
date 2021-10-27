using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MovieApp.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;

        public HomeController(IMovieService movieService, ICategoryService categoryService)
        {
            _movieService = movieService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5)
        {
            var moviesResult = await (categoryId == null
                ? _movieService.GetAllPagingAsync(null, currentPage, pageSize)
                : _movieService.GetAllPagingAsync(categoryId.Value, currentPage, pageSize));
            return View(moviesResult.Data);
        }
        public  IActionResult ComingSoon()
        {
            return View("ComingSoonIndex");
        }
    }
}
