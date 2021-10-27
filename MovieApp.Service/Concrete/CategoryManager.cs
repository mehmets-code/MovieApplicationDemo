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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Category>> AddAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<Category>(ResultStatus.Success, Messages.Category.Add(category.Name), category);

        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata karşılaşıldı.", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.Delete(category.Name));
            }
            return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural:false));
        }

        public async Task<IDataResult<Category>> GetAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }
            return new DataResult<Category>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<IList<Category>>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success,categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), categories);
        }

        public async Task<IDataResult<Category>> UpdateAsync(Category category)
        {
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<Category>(ResultStatus.Success, Messages.Category.Update(category.Name), category);
        }
    }
}
