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
        public string Image { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime TimeBegin { get; set; }
        public string BuyerName { get; set; }

        public virtual ICollection<OrmImage> OrmImages { get; set; }


        //куплено,в процессе,не выставлено
        public int OrmStatysId { get; set; }
        public virtual OrmStatys OrmStatys { get; set; }


        //внешний ключ
        //http://metanit.com/sharp/entityframework/3.2.php

        //Имя_навигационного_свойства+Имя ключа из связанной таблицы 
        //или
        //Имя_класса_связанной_таблицы+Имя ключа из связанной таблицы
        //внешний ключ
        public int OrmCathegoryId { get; set; }
        //навигационное свойство
        public virtual OrmCathegory OrmCathegory { get; set; }




        //внешний ключ
        public int OrmUserId { get; set; }
        //навигационное свойство 
        public virtual OrmUser OrmUser { get; set; }



    }
}
