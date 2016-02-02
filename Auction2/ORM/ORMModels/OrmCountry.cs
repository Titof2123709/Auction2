using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
    [Table("Country")]
    public partial class OrmCountry
    {
        public OrmCountry()
        {
            OrmProfiles = new HashSet<OrmProfile>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<OrmProfile> OrmProfiles { get; set; }
    }
}
