using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities.Concrete;
using MovieApp.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMovieService _categoryMovieService;
        public CategoryController(ICategoryService categoryService,ICategoryMovieService categoryMovieService)
        {
            _categoryService = categoryService;
            _categoryMovieService = categoryMovieService;
        }
        [Authorize(Roles = "SuperAdmin,Category.Read")]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            return View(result.Data);
        }
        //Get//Add Category
        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpGet]
        public  IActionResult Add()
        {
            ViewBag.ActionMode = "Add";
            return View(new Category());
        }
        //Post//Add Category
        [Authorize(Roles = "SuperAdmin,Category.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(Category category )
        {
             await _categoryService.AddAsync(category);
            return RedirectToAction(nameof(Index), category);
        }
        //Get//Update Category
        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetAsync(id);
            ViewBag.ActionMode = "Update";
            return View("Add",category.Data);
        }
        //Post//Update Category
        [Authorize(Roles = "SuperAdmin,Category.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            await _categoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index), category);
        }
        [Authorize(Roles = "SuperAdmin,Category.Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            await _categoryMovieService.DeleteMoviesAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
