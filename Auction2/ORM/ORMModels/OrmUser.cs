using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
    [Table("User")]
    public partial class OrmUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public DateTime TimeRegister { get; set; }


        public OrmUser()
        {
            OrmLots = new HashSet<OrmLot>();
            OrmRoles = new HashSet<OrmRole>();
        }
        public virtual ICollection<OrmLot> OrmLots { get; set; }
        //связь многие ко многим
        public virtual ICollection<OrmRole> OrmRoles { get; set; }

        public virtual OrmProfile OrmProfile { get; set; }
    }
}
