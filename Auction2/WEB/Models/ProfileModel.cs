using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Receiver { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [StringLength(10)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [RegularExpression("[0-9]{7,18}", ErrorMessage = "Phone должен содержать  цифры от 7 до 12 символов")]
        public int Phone { get; set; }
    }
}