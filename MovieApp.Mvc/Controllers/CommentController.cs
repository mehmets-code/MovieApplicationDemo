using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities.Dtos;
using MovieApp.Service.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentAddDto commentAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentService.AddAsync(commentAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    int MovieId = commentAddDto.MovieId;
                    return RedirectToAction("MovieDetail","Movie",MovieId /*new CommentDto { Comment = result.Data.Comment}*/);
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(new CommentDto { Comment = null });

        }

    }
}
