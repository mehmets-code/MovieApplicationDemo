using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IMovieRepository Movies { get; }
        ICategoryRepository Categories { get; }
        ICategoryMovieRepository CategoryMovies { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
        Task<int> SaveAsync();
        
    }
}
