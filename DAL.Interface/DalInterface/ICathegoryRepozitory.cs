using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface ICathegoryRepository:IRepository<DalCathegory>
    {
        int GetIdByName(string name);
        IEnumerable<DalLot> GetLotsByCathegoryName(string name);
        IEnumerable<DalLot> GetLotsByCathegoryNameAndStatysInProcess(string name);
        IEnumerable<string> GetAllNames();
        string GetCathegoryNameById(int id);

    }
}
