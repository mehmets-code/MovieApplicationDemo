using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Abstract;
using MovieApp.Data.Concrete.EntityFramework.Contexts;
using MovieApp.Entities.Concrete;
using MovieApp.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
            
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await MovieContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        private MovieContext MovieContext
        {
            get
            {
                return _context as MovieContext;
            }
        }
    }
}
