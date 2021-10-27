using MovieApp.Entities.Concrete;
using MovieApp.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Abstract
{
    public interface IMovieRepository:IEntityRepository<Movie>
    {
    }
}
