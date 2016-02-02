using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string MimeType { get; set; }
        public int LotId { get; set; }
    }
}