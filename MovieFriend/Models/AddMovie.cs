using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFriend.Models
{
    public class AddMovie
    {
        [Required(ErrorMessage = "Введите ссылку")]
        public string Images { get; set; }
        [Required(ErrorMessage = "Введите ссылку")]
        public string Trailer { get; set; }
        [Required(ErrorMessage = "Введите Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите рейтинг")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Введите отзыв")]
        public string Reviews { get; set; }
        //[Required(ErrorMessage = "Введите имя")]
        public string NameAuthor { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Called { get; set; }
    }
}
