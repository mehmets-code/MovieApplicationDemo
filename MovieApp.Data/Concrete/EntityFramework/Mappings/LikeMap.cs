using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Concrete.EntityFramework.Mappings
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();
            builder.HasOne<User>(l => l.User).WithMany(u => u.Likes).HasForeignKey(l => l.UserId);
            builder.HasOne<Movie>(l => l.Movie).WithMany(m => m.Likes).HasForeignKey(l => l.MovieId);
            builder.ToTable("Likes");
            builder.HasData(new Like
            {
                Id = 1,
                MovieId = 1,
                UserId = 1,
            });
        }
    }
}
