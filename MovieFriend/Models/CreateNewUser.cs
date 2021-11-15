using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFriend.Models
{
    public class CreateNewUser
    {
        [Required(ErrorMessage ="Введите имя")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage ="Длинна пароля должна быть не меньше 6 символов")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите Email")]
        [MinLength(6, ErrorMessage = "Email не может быть меньше 6 символов")]
        public string Email { get; set; }
    }
}
