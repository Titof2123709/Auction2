using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
    [Table("Image")]
    public class OrmImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }



        public int OrmLotId { get; set; }
        public virtual OrmLot OrmLot { get; set; }

    }
}
