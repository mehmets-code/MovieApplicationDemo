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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MovieManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<MovieAddDto>> AddAsync(MovieAddDto movieAddDto)
        {
            var movie = _mapper.Map<Movie>(movieAddDto);
            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.SaveAsync();
            movieAddDto.Id = movie.Id;
            return new DataResult<MovieAddDto>(ResultStatus.Success,Messages.Movie.Add(movie.Title),movieAddDto);
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var moviesCount = await _unitOfWork.Movies.CountAsync();
            if (moviesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, moviesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata karşılaşıldı.", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int movieId)
        {
            var result = await _unitOfWork.Movies.AnyAsync(m => m.Id == movieId);
            if (result)
            {
                var movie = await _unitOfWork.Movies.GetAsync(m => m.Id == movieId);
                await _unitOfWork.Movies.DeleteAsync(movie);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Movie.Delete(movie.Title));
            }
            return new Result(ResultStatus.Error," Böyle bir makale bulunamadı.");
        }

        public async Task<IDataResult<MovieDto>> GetAsync(int movieId)
        {
            var movie = await _unitOfWork.Movies.GetAsync(m => m.Id == movieId, m => m.CategoryMovies, m=>m.Comments);
            if (movie != null)
            {
                return new DataResult<MovieDto>(ResultStatus.Success, new MovieDto
                {
                    Movie = movie,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<MovieDto>(ResultStatus.Error, Messages.Movie.NotFound(isPlural:false), null);
        }

        public async Task<IDataResult<MovieListDto>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync(null, m => m.CategoryMovies);
            if (movies.Count() > -1)
            {
                return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
                {
                    Movies = movies,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<MovieListDto>(ResultStatus.Error, Messages.Movie.NotFound(isPlural: true), null);
        }
        public async Task<IDataResult<MovieListDto>> GetAllPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var movies = categoryId == null
                ? await _unitOfWork.Movies.GetAllAsync(null, m => m.CategoryMovies)
                : await _unitOfWork.Movies.GetAllAsync(m => m.CategoryMovies.FirstOrDefault(cm=>cm.CategoryId == categoryId).CategoryId == categoryId,
                   m => m.CategoryMovies);
            var sortedMovies = isAscending
                ? movies.OrderBy(m => m.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : movies.OrderByDescending(m => m.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
            {
                Movies = sortedMovies,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = movies.Count
            });
        }
        public async Task<IDataResult<MovieListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c=>c.Id == categoryId);
            if (result)
            {
                var movies = await _unitOfWork.Movies.GetAllAsync(
                   /* m => m.Categories.Where(c => c.Id == categoryId).ToList().Count()!=0*/);
                return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
                {
                    Movies = movies,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<MovieListDto>(ResultStatus.Error, Messages.Movie.NotFound(isPlural: true), null);
        }
        public async Task<IDataResult<MovieListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            var movies =
                await  _unitOfWork.Movies.GetAllAsync(null,m => m.CategoryMovies);
            var sortedArticles = isAscending
                ? movies.OrderBy(a => a.ViewCount)
                : movies.OrderByDescending(a => a.ViewCount);
            return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
            {
                Movies = takeSize == null ? sortedArticles.ToList() : sortedArticles.Take(takeSize.Value).ToList()
            });
        }
        public async Task<IDataResult<MovieUpdateDto>> UpdateAsync(MovieUpdateDto movieUpdateDto)
        {
            var movie = _mapper.Map<Movie>(movieUpdateDto);
            await _unitOfWork.Movies.UpdateAsync(movie);
            await _unitOfWork.SaveAsync();
            return new DataResult<MovieUpdateDto>(ResultStatus.Success, Messages.Movie.Update(movie.Title), movieUpdateDto);
        }

        public async Task<IDataResult<MovieListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var movies =
                    await _unitOfWork.Movies.GetAllAsync(null, m => m.CategoryMovies);
                var sortedArticles = isAscending
                    ? movies.OrderBy(a => a.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : movies.OrderByDescending(a => a.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
                {
                    Movies = sortedArticles,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = movies.Count
                });
            }

            var searchedMovies = await _unitOfWork.Movies.SearchAsync(new List<Expression<Func<Movie, bool>>>
            {
                (m) => m.Title.Contains(keyword)
            },
            m => m.CategoryMovies);
            var searchedAndSortedArticles = isAscending
                ? searchedMovies.OrderBy(a => a.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : searchedMovies.OrderByDescending(a => a.Year).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<MovieListDto>(ResultStatus.Success, new MovieListDto
            {
                Movies = searchedAndSortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchedMovies.Count,
            });
        }
    }
}
