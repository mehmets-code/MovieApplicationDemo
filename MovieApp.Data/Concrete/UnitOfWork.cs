using MovieApp.Data.Abstract;
using MovieApp.Data.Concrete.EntityFramework.Contexts;
using MovieApp.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;
        private ICategoryRepository _categoryRepository;
        private IMovieRepository _movieRepository;
        private ICategoryMovieRepository _categoryMovieRepository;
        public UnitOfWork(MovieContext context)
        {
            _context = context;
        }
        public IMovieRepository Movies => _movieRepository ?? new EfMovieRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);
        public ICategoryMovieRepository CategoryMovies => _categoryMovieRepository ?? new EfCategoryMovieRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);


        public ILikeRepository Likes => _likeRepository ?? new EfLikeRepository(_context);

       

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
