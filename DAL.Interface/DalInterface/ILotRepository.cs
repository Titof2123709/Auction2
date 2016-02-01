using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface ILotRepository : IRepository<DalLot>
    {
        IEnumerable<string> GetAllLotNameByUserId(int id);
        IEnumerable<DalLot> GetAllLotNameByTime(TimeSpan time);
        DalLot GetLotByName(string name);
        void Delete(string name);

        IEnumerable<DalLot> GetLotsByCathegoryId(int key);
        IEnumerable<DalLot> GetAllExposeLots();
        IEnumerable<DalLot> GetLotsByName(string name);
        IEnumerable<DalLot> FindLotsByUserName(string name);
        void Delete(int Id);
    }
}
