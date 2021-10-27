using Microsoft.AspNetCore.Identity;
using MovieApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Concrete
{
    public class User:IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
