using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _commentService.GetAllAsync();
            return View(result.Data);
        }
    }
}
