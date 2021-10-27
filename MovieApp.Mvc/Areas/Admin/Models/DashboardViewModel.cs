using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int MoviesCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public MovieListDto Movies { get; set; }
    }
}
