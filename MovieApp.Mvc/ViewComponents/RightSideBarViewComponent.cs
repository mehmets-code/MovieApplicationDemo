using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Mvc.Models;
using MovieApp.Service.Abstract;

namespace MovieApp.Mvc.ViewComponents
{
    public class RightSideBarViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMovieService _movieService;

        public RightSideBarViewComponent(ICategoryService categoryService, IMovieService movieService)
        {
            _categoryService = categoryService;
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllAsync();
            var moviesResult = await _movieService.GetAllByViewCountAsync(isAscending: false, takeSize: 5);
            return View(new RightSideBarViewModel
            {
                Categories = categoriesResult.Data,
                Movies = moviesResult.Data.Movies
            });
        }
    }
}
