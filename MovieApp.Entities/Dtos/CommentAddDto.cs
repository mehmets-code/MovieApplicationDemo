using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Dtos
{
    public class CommentAddDto
    {
        [DisplayName("Yorum")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(1000, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string Content { get; set; }
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string UserName { get; set; }
    }
}
