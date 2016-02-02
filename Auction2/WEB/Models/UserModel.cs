using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression("[a-zA-Z0-9]{2,12}", ErrorMessage = "Имя должно содержать буквы и цифры от 2 до 12 символов")]
        public string Name { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Неверный формат даты: YYYY-MM-DD")]
        public DateTime TimeRegister { get; set; }

    }
}