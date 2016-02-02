using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
    [Table("Role")]
    public partial class OrmRole
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public OrmRole()
        {
            OrmUsers = new HashSet<OrmUser>();
        }

        //связь многие ко многим
        public virtual ICollection<OrmUser> OrmUsers { get; set; }
    }
}
