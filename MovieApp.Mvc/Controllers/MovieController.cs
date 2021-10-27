using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Mvc.Models;
using MovieApp.Service.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public MovieController(IMovieService movieService,UserManager<User> userManager,IMapper mapper)
        {
            _movieService = movieService;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var searchResult = await _movieService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Success)
                return View(new MovieSearchViewModel
                {
                    MovieListDto = searchResult.Data,
                    Keyword = keyword
                });
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> MovieDetail(int movieId)
        {
            var movieResult = await _movieService.GetAsync(movieId);
            var User = await _userManager.GetUserAsync(HttpContext.User);
            if (User != null)
            {
                movieResult.Data.UserId = User.Id;
                movieResult.Data.UserName = User.UserName;
            }
            else
            {
                movieResult.Data.UserId = null;
                movieResult.Data.UserName = null;
            }                   
            if (movieResult.ResultStatus == ResultStatus.Success)
            {
                return View(movieResult.Data);
            }

            return NotFound();
        }
    }
}
