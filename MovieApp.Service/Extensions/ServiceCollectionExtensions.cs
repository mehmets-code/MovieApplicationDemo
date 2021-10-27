using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Data.Abstract;
using MovieApp.Data.Concrete;
using MovieApp.Data.Concrete.EntityFramework.Contexts;
using MovieApp.Entities.Concrete;
using MovieApp.Service.Abstract;
using MovieApp.Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User Password Options;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //User Username and Email Options
                options.User.AllowedUserNameCharacters= options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;
            }
                ).AddEntityFrameworkStores<MovieContext>();
            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);
            });
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<ICategoryMovieService, CategoryMovieManager>();
            serviceCollection.AddScoped<IMovieService, MovieManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();
            return serviceCollection;
        }
    }
}
