using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Entities.Concrete;

namespace MovieApp.Mvc.Models
{
    public class RightSideBarViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}
