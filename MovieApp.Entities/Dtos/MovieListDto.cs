using MovieApp.Entities.Concrete;
using MovieApp.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Dtos
{
    public class MovieListDto:DtoGetBase
    {
        public IList<Movie> Movies { get; set; }
        public int? CategoryId { get; set; }
    }
}
