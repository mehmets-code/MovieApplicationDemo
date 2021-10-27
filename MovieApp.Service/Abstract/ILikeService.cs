using MovieApp.Entities.Concrete;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Abstract
{
    public interface ILikeService
    {
        Task<IDataResult<Like>> AddAsync(Like like);
        Task<IResult> DeleteAsync(int likeId);
        Task<IDataResult<int>> LikesCountByMovieAsync(int movieId);
    }
}
