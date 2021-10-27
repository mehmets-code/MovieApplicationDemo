using MovieApp.Entities.Concrete;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<Category>> GetAsync(int categoryId);
        Task<IDataResult<IList<Category>>> GetAllAsync();
        Task<IDataResult<Category>> AddAsync(Category category );
        Task<IDataResult<Category>> UpdateAsync(Category category );
        Task<IResult> DeleteAsync(int categoryId);
        Task<IDataResult<int>> CountAsync();
    }
}
