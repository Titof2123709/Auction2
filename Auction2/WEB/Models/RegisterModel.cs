﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Введите имя")]
        [RegularExpression("[a-zA-Z0-9]{2,12}", ErrorMessage = "Имя должно содержать буквы и цифры от 2 до 12 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [RegularExpression("[.\\-_a-z0-9]{6,18}", ErrorMessage = "Пароль должен содержать символы .\\-_ буквы и цифры от 2 до 12 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}