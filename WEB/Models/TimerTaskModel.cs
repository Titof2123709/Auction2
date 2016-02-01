using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WEB.Models
{
    public class TimerTaskModel
    {
        public Task Timer { get; set; }
        public string GroupName { get; set; }
        public string Message { get; set; }
    }
}