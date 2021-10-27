using MovieApp.Data.Abstract;
using MovieApp.Entities.Concrete;
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
    public class CategoryMovieManager : ICategoryMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryMovieManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Entities.Concrete.MovieCategory>> add(Entities.Concrete.MovieCategory movieCategory)
        {
            await _unitOfWork.CategoryMovies.AddAsync(movieCategory);
            await _unitOfWork.SaveAsync();
            return new DataResult<MovieCategory>(ResultStatus.Success, "Filmler ve kategoriler başarıyla eklenmiştir.", movieCategory);
        }

        public async Task<IResult> DeleteCategoriesAsync(int movieId)
        {
            var result = await _unitOfWork.CategoryMovies.AnyAsync(c => c.MovieId == movieId);
            if (result)
            {
                var categoryMovie = await _unitOfWork.CategoryMovies.GetAllAsync(c => c.MovieId == movieId);
                foreach(var category in categoryMovie)
                {
                    await _unitOfWork.CategoryMovies.DeleteAsync(category);               
                }
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Filmin kategorileri başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Filmin kategorileri silinememiştir.");
        }

        public async Task<IResult> DeleteMoviesAsync(int categoryId)
        {
            var result = await _unitOfWork.CategoryMovies.AnyAsync(c => c.CategoryId == categoryId);
            if (result)
            {
                var categoryMovie = await _unitOfWork.CategoryMovies.GetAsync(c => c.CategoryId == categoryId);
                await _unitOfWork.CategoryMovies.DeleteAsync(categoryMovie);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Kategorinin filmleri başarıyla silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Kategorinin filmleri başarıyla silinmiştir.");
        }
    }
}
