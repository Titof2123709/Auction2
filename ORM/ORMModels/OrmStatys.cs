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
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrmStatys()
        {
            OrmLots = new HashSet<OrmLot>();
        }

     //   [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

     //   [StringLength(50)]
        public string Name { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrmLot> OrmLots { get; set; }
    }
}
