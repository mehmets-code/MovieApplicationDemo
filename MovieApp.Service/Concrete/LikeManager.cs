using MovieApp.Data.Abstract;
using MovieApp.Entities.Concrete;
using MovieApp.Service.Abstract;
using MovieApp.Shared.Utilities.Results.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using MovieApp.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Concrete
{
    class LikeManager : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikeManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<Like>> AddAsync(Like like)
        {
            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.SaveAsync();
            return new DataResult<Like>(ResultStatus.Success, "Like başarıyla eklendi", like);
        }

        public async Task<IResult> DeleteAsync(int likeId)
        {
            var result = await _unitOfWork.Likes.AnyAsync(l => l.Id == likeId);
            if (result)
            {
                var like = await _unitOfWork.Likes.GetAsync(l => l.Id == likeId);
                await _unitOfWork.Likes.DeleteAsync(like);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Like başarıyla kaldırıldı.");
            }
            return new Result(ResultStatus.Error, "Yorum başarıyla kaldırılamadı.");
        }

        public async Task<IDataResult<int>> LikesCountByMovieAsync(int movieId)
        {
            var likesCount = await _unitOfWork.Likes.CountAsync(l => l.MovieId == movieId);
            if (likesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, likesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata karşılaşıldı.", -1);
            }
        }
    }
}
