using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.ORMModels
{
   [Table("Profile")]
   public partial class OrmProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Receiver { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public virtual OrmUser OrmUser { get; set; }


        public int OrmCountryId { get; set; }
        public virtual OrmCountry OrmCountry { get; set; }
    }
}
