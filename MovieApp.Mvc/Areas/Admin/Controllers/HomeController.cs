using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Entities.Concrete;
using MovieApp.Mvc.Areas.Admin.Models;
using MovieApp.Service.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMovieService _movieService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService, IMovieService movieService, ICommentService commentService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _movieService = movieService;
            _commentService = commentService;
            _userManager = userManager;
        }
        [Authorize(Roles = "SuperAdmin,AdminArea.Home.Read")]
        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountAsync();
            var moviesCountResult = await _movieService.CountAsync();
            var commentsCountResult = await _commentService.CountAsync();
            var usersCount =  _userManager.Users.Count();
            var articlesResult = await _movieService.GetAllAsync();
            if (categoriesCountResult.ResultStatus == ResultStatus.Success && moviesCountResult.ResultStatus == ResultStatus.Success && commentsCountResult.ResultStatus == ResultStatus.Success && usersCount > -1 && articlesResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCountResult.Data,
                    MoviesCount = moviesCountResult.Data,
                    CommentsCount = commentsCountResult.Data,
                    UsersCount = usersCount,
                    Movies = articlesResult.Data
                });
            }

            return NotFound();

        }
    }
}
