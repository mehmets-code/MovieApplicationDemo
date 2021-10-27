using MovieApp.Entities.Concrete;
using MovieApp.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Dtos
{
    public class MovieDto:DtoGetBase
    {
        public Movie Movie { get; set; }
        public int? UserId { get; set; }
        public string  UserName { get; set; }
    }
}
