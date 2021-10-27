using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Concrete.EntityFramework.Mappings;
using MovieApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Concrete.EntityFramework.Contexts
{
    public class MovieContext:IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MovieCategory> CategoryMovie { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.Entity<MovieCategory>().HasKey(cm => new { cm.CategoryId, cm.MovieId });
            modelBuilder.Entity<MovieCategory>()
            .HasOne<Movie>(cm => cm.Movie)
            .WithMany(m => m.CategoryMovies)
            .HasForeignKey(cm => cm.MovieId);

            modelBuilder.Entity<MovieCategory>()
                .HasOne<Category>(cm => cm.Category)
                .WithMany(c => c.CategoryMovies)
                .HasForeignKey(cm => cm.CategoryId);
            modelBuilder.Entity<MovieCategory>().HasKey(cm => new { cm.CategoryId, cm.MovieId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
