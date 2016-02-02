using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.BLLModel
{
    public class BllImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public int LotId { get; set; }

    }
}
