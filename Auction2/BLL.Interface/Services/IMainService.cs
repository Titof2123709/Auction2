using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IMainService
    {
        IEnumerable<BllLot> FindAllExposeLots();
        IEnumerable<BllLot> FindLotsByCathegoryNameAndStatysInProcess(string name);
        IEnumerable<string> GetAllNames();
        IEnumerable<BllLot> FindLotByName(string name);
    }
}
