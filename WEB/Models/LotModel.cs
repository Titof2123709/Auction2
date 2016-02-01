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
        [Range(1, 1000000000, ErrorMessage = "Цена задана некорректно")]
        public double StartPrice { get; set; }

        //это не работает т.к не везде используется!!
  //      [Range(1, 1000000000, ErrorMessage = "Цена задана некорректно")]
        public double EndPrice { get; set; }

    /*    [Range(1, 1000000000, ErrorMessage = "Цена задана некорректно")]
        public double EndPrice { get; set; }
        */
        public string Image { get; set; }

        [Required(ErrorMessage = "Введите дату")]
       // [RegularExpression("[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])", ErrorMessage = "Неверный формат даты: YYYY-MM-DD")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты: YYYY-MM-DD")]
        public DateTime DateBegin { get; set; }

        [Required(ErrorMessage = "Введите время")]
      //  [RegularExpression("([0-1]\\d|2[0-3])(:[0-5]\\d){2}", ErrorMessage = "Неверный формат времени: HH:MM:SS")]
        [DataType(DataType.Time,ErrorMessage = "Неверный формат времени: HH:MM:SS")]
        public DateTime TimeBegin { get; set; }

        public string BuyerName { get; set; }

        public Statys Statys { get; set; }

        public string Cathegory { get; set; }
    }
}