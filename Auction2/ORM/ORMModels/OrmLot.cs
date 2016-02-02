using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
   [Table("Lot")]
    public partial class OrmLot
    {
        public OrmLot()
        {
            OrmImages = new HashSet<OrmImage>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime TimeBegin { get; set; }
        public string BuyerName { get; set; }

        public virtual ICollection<OrmImage> OrmImages { get; set; }



        public int OrmStatysId { get; set; }
        public virtual OrmStatys OrmStatys { get; set; }



        public int OrmCathegoryId { get; set; }
        public virtual OrmCathegory OrmCathegory { get; set; }




        public int OrmUserId { get; set; }
        public virtual OrmUser OrmUser { get; set; }



    }
}
