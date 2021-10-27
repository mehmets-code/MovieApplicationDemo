using MovieApp.Entities.Concrete;
using MovieApp.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Dtos
{
    public class UserDto : DtoGetBase
    {
        public User User { get; set; }
        public IList<string> userRoles { get; set; }
    }
}
