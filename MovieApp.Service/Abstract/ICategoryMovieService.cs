using MovieApp.Entities.Concrete;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Abstract
{
    public interface ICategoryMovieService
    {
        Task<IResult> DeleteMoviesAsync(int categoryId);
        Task<IResult> DeleteCategoriesAsync(int movieId);
        Task<IDataResult<MovieCategory>> add(MovieCategory movieCategory);
    }
}
