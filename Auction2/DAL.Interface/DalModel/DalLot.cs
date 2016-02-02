using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalModel
{
    public class DalLot : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime TimeBegin { get; set; }
        public string BuyerName { get; set; }
        public int StatysId { get; set; }
        public int CathegoryId { get; set; }
        public int UserId { get; set; }
    }
}
