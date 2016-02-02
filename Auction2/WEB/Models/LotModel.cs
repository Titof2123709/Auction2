using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public enum Statys
    {
        NotExpose=1,/*не выставлено*/
        InProcess,
        Sold
    }
    public class LotModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9-_\\.]{1,20}", ErrorMessage = "Имя должно содержать буквы и цифры от 2 до 12 символов")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Описание лота слишком большое")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Range(1, 1000000, ErrorMessage = "Цена задана некорректно")]
        [Display(Prompt="$")]
        public double StartPrice { get; set; }


        [Display(Prompt = "$")]
        public double EndPrice { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты: YYYY-MM-DD")]
        public DateTime DateBegin { get; set; }

        [Required(ErrorMessage = "Введите время")]
        [DataType(DataType.Time,ErrorMessage = "Неверный формат времени: HH:MM:SS")]
        public DateTime TimeBegin { get; set; }

        public string BuyerName { get; set; }

        public Statys Statys { get; set; }

        [Required]
        public string Cathegory { get; set; }
    }
}