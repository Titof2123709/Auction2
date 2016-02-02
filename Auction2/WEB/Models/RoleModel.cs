using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите роль")]
        [RegularExpression("[a-zA-Z0-9]{2,12}", ErrorMessage = "Роль должна содержать буквы и цифры от 2 до 12 символов")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(50)]
        public string Description { get; set; }
    }
}