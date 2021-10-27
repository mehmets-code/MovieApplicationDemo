using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<CommentListDto>> GetCommentsByMovieAsync(int movieId);
        Task<IDataResult<CommentListDto>> GetAllAsync();
        Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto);
        Task<IResult> DeleteAsync(int commentId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CommentsCountByMovieAsync(int movieId); 

    }
}
