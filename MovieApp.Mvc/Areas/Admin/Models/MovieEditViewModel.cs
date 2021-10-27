using Microsoft.AspNetCore.Http;
using MovieApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Models
{
    public class MovieEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Title { get; set; }
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Description { get; set; }
        [DisplayName("Küçük Resim")]
        public string Thumbnail { get; set; }
        [DisplayName("Küçük Resim Ekle")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public IFormFile ThumbnailFile { get; set; }
        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Başrol Oyuncuları")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Headliners { get; set; }
        [DisplayName("Kategoriler")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public IList<Category> Categories { get; set; }
        public IList<string> categori { get; set; }
    }
}
