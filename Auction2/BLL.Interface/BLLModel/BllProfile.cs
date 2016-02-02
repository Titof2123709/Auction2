﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.BLLModel
{
    public class BllProfile
    {
        public int Id { get; set; }
        public string Receiver { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int CountryId { get; set; }
    }
}
