using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities.ComplexTypes;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Mvc.Areas.Admin.Models;
using MovieApp.Mvc.Helpers;
using MovieApp.Mvc.Helpers.Abstract;
using MovieApp.Service.Abstract;
using MovieApp.Service.Utilities;
using MovieApp.Shared.Utilities.Results.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using MovieApp.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMovieService _categoryMovieService;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;
        public MovieController(IMovieService movieService, IMapper mapper,ICategoryService categoryService,IImageHelper imageHelper
            ,ICategoryMovieService categoryMovieService)
        {
            _movieService = movieService;
            _categoryService = categoryService;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _categoryMovieService = categoryMovieService;
        }
        [Authorize(Roles = "SuperAdmin,Movie.Read")]
        public async Task<IActionResult> Index()
        {
            var result = await _movieService.GetAllAsync();
            return View(result.Data.Movies);
        }
        [Authorize(Roles = "SuperAdmin,Movie.Create")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.ActionMode = "Add";
            var result = await _categoryService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View("Edit", new MovieEditViewModel
                {
                    Categories = result.Data
                });
            }
            return NotFound();
        }
        [Authorize(Roles = "SuperAdmin,Movie.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(MovieEditViewModel movieEditViewModel)
        {
            var movieAddDto = _mapper.Map<MovieAddDto>(movieEditViewModel);
            foreach (var categoryId in movieEditViewModel.categori )
            {
                var movieCategory = await _categoryService.GetAsync(Convert.ToInt32(categoryId));
                movieAddDto.Categories.Add(movieCategory.Data);
            }
           
            
            var imageResult = await _imageHelper.Upload(movieEditViewModel.Title,
                   movieEditViewModel.ThumbnailFile,PictureType.Movie);
            movieAddDto.Thumbnail = imageResult.Data.FullName;
            
            var result = await _movieService.AddAsync(movieAddDto);
          //  var movie = _mapper.Map<Movie>(movieAddDto);
            foreach (var categoryId in movieAddDto.categori)
            {
                var movieCategory = new MovieCategory();
                movieCategory.CategoryId = Convert.ToInt32(categoryId);
                var category = _categoryService.GetAsync(movieCategory.CategoryId);
                movieCategory.Category = category.Result.Data;
                movieCategory.MovieId = result.Data.Id;
                var result2 = await _categoryMovieService.add(movieCategory);
              //  movieAddDto.Categories.Add(movieCategory.Data);
            }
            return RedirectToAction(nameof(Index), movieAddDto);
        }
        [Authorize(Roles = "SuperAdmin,Movie.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var movie = await _movieService.GetAsync(id);
            ViewBag.ActionMode = "Update";
            var result = await _categoryService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View("Edit", new MovieEditViewModel
                {
                    Categories = result.Data,
                    Title = movie.Data.Movie.Title,
                    Description = movie.Data.Movie.Description,
                    Date = movie.Data.Movie.Year,
                    Headliners = movie.Data.Movie.Headliners
                });
            }
            return NotFound();
        }
        [Authorize(Roles = "SuperAdmin,Movie.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(MovieEditViewModel movieEditViewModel)
        {
            var movieUpdateDto = _mapper.Map<MovieUpdateDto>(movieEditViewModel);
            foreach (var categoryId in movieEditViewModel.categori)
            {
                var movieCategory = await _categoryService.GetAsync(Convert.ToInt32(categoryId));
                movieUpdateDto.Categories.Add(movieCategory.Data);
            }


            var imageResult = await _imageHelper.Upload(movieEditViewModel.Title,
                   movieEditViewModel.ThumbnailFile,PictureType.Movie);
            movieUpdateDto.Thumbnail = imageResult.Data.FullName;
            var result = await _categoryMovieService.DeleteCategoriesAsync(movieUpdateDto.Id);
            var result2 = await _movieService.UpdateAsync(movieUpdateDto);
            foreach (var categoryId in movieUpdateDto.categori)
            {
                var movieCategory = new MovieCategory();
                movieCategory.CategoryId = Convert.ToInt32(categoryId);
                movieCategory.MovieId = result2.Data.Id;
                var result3 = await _categoryMovieService.add(movieCategory);
                //  movieAddDto.Categories.Add(movieCategory.Data);
            }
            return RedirectToAction(nameof(Index), movieUpdateDto);
        }
        [Authorize(Roles = "SuperAdmin,Movie.Delete")]
        public async Task<IActionResult> Delete(int id)
        {
          //  var movie = await _movieService.GetAsync(id);
            var result = await _categoryMovieService.DeleteCategoriesAsync(id);
            var result2 = await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
