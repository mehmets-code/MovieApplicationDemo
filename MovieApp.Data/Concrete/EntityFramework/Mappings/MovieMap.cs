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
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.Title).HasMaxLength(100);
            builder.Property(m => m.Title).IsRequired();
            builder.Property(m => m.Description).HasMaxLength(500);
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.Year).IsRequired();
            builder.Property(m => m.Headliners).HasMaxLength(250);
            builder.Property(m => m.Thumbnail).HasMaxLength(250);
            builder.Property(m => m.Thumbnail).IsRequired();
            builder.Property(m => m.LikeCount).IsRequired();
            builder.Property(m => m.CommentCount).IsRequired();
            builder.Property(m => m.ViewCount).IsRequired();
            builder.Property(m => m.CreatedDate).HasMaxLength(50);
            builder.Property(m => m.CreatedDate).IsRequired();
            builder.Property(m => m.ModifiedDate).HasMaxLength(50);
            builder.Property(m => m.ModifiedDate).IsRequired();
            builder.ToTable("Movies");
            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Shang-Chi ve on Halka Efsanesi",
                    Description = "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>",
                    Thumbnail = "movieImages/defaultThumbnail.jpg",
                    Year = DateTime.Today,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ViewCount = 125,
                    CommentCount = 3,
                    LikeCount = 37
                },
                new Movie
                {
                    Id = 2,
                    Title = "Dune: Çöl Gezegeni",
                    Description = "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>",
                    Thumbnail = "movieImages/defaultThumbnail.jpg",
                    Year = DateTime.Today,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ViewCount = 195,
                    CommentCount = 7,
                    LikeCount = 88
                },
                new Movie
                {
                    Id = 3,
                    Title = "Örümcek-Adam: Eve Dönüş Yok",
                    Description = "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>",
                    Thumbnail = "movieImages/defaultThumbnail.jpg",
                    Year = DateTime.Today,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ViewCount = 272,
                    CommentCount = 14,
                    LikeCount = 118
                });
        }
    }
}
