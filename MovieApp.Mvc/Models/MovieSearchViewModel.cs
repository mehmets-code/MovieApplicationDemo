using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Models
{
    public class MovieSearchViewModel
    {
        public MovieListDto MovieListDto { get; set; }
        public string Keyword { get; set; }
    }
}
