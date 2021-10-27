using MovieApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Concrete
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
    }
}
