using MovieApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Concrete
{
    public class Movie: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public string Headliners { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public ICollection<MovieCategory> CategoryMovies { get; set; }
    }
}
