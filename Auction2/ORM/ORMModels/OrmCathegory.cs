using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
  [Table("Cathegory")]
  public partial class OrmCathegory
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public OrmCathegory()
            {
                OrmLots = new HashSet<OrmLot>();
            }
            public virtual ICollection<OrmLot> OrmLots { get; set; }
        }
}
