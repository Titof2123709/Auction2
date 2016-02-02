using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
    [Table("Statys")]
    public partial class OrmStatys
    {
        public OrmStatys()
        {
            OrmLots = new HashSet<OrmLot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<OrmLot> OrmLots { get; set; }
    }
}
