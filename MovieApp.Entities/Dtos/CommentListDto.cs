using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Entities.Concrete;

namespace MovieApp.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
