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
        //поля пользователя
    //    [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public DateTime TimeRegister { get; set; }

        //пользователь содержит коллекцию лотов
        // "ленивая загрузка" или lazy loading.
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
/*
 Если тип обычного свойства во внешнем ключе
  определяется как public int? RoleId { get; set; }, 
  то есть допускает значения null,
  то при создании базы данных соответствующее поле так
  будет принимать значения NULL: [TeamId] INT NULL.
 
  Однако если мы изменим на просто int:
public int RoleId { get; set; }, то в этом случае соответствующее 
поле имело бы ограничение NOT NULL, а внешний ключ определял бы каскадное удаление
 */
