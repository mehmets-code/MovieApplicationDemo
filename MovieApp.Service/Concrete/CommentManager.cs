using AutoMapper;
using MovieApp.Data.Abstract;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Service.Abstract;
using MovieApp.Service.Utilities;
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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var movie = await _unitOfWork.Movies.GetAsync(a => a.Id == commentAddDto.MovieId);
            if (movie == null)
            {
                return new DataResult<CommentDto>(ResultStatus.Error, Messages.Movie.NotFound(isPlural: false), null);
            }
            var comment = _mapper.Map<Comment>(commentAddDto);
            await _unitOfWork.Comments.AddAsync(comment);
            movie.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.MovieId == movie.Id );
            await _unitOfWork.Movies.UpdateAsync(movie);
            await _unitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success,"Yorum başarıyla eklendi.", new CommentDto
            {
                Comment =comment
            });
        }
        public async Task<IDataResult<int>> CommentsCountByMovieAsync(int movieId)
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(c=>c.MovieId == movieId);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata karşılaşıldı.", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int commentId)
        {
            var result = await _unitOfWork.Comments.AnyAsync(c => c.Id == commentId);
            if (result)
            {
                var comment = await _unitOfWork.Comments.GetAsync(c=>c.Id == commentId);
                await _unitOfWork.Comments.DeleteAsync(comment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Yorum başarıyla silindi.");
            }
            return new Result(ResultStatus.Error, "Yorum başarıyla silinemedi.");
        }

        public async Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(null, c => c.Movie);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, "Yorumlar bulunamadı.", new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetCommentsByMovieAsync(int movieId)
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(c => c.MovieId == movieId);
            if (comments.Count() > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Success, new CommentListDto
                {
                    Comments = comments
                });
            }
            return new DataResult<CommentListDto>(ResultStatus.Error, "Yorum bulunamadı.", null);
        }

        public async Task<IDataResult<Comment>> UpdateAsync(Comment comment)
        {
            await _unitOfWork.Comments.UpdateAsync(comment);
            await _unitOfWork.SaveAsync();
            return new DataResult<Comment>(ResultStatus.Success, "Yorum başarıyla güncellendi", comment);
        }
    }
}
