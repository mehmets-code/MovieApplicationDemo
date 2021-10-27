using MovieApp.Entities.Dtos;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Abstract
{
    public interface IMovieService
    {
        Task<IDataResult<MovieDto>> GetAsync(int movieId);
        Task<IDataResult<MovieListDto>> GetAllAsync();
        Task<IDataResult<MovieListDto>> GetAllPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<IDataResult<MovieListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IDataResult<MovieListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize);
        Task<IDataResult<MovieListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5,
           bool isAscending = false);
        Task<IDataResult<MovieAddDto>> AddAsync(MovieAddDto movieAddDto);
        Task<IDataResult<MovieUpdateDto>> UpdateAsync(MovieUpdateDto movieUpdateDto);
        Task<IResult> DeleteAsync(int movieId);
        Task<IDataResult<int>> CountAsync();
    }
}
