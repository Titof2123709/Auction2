using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ILotService
    {
        IEnumerable<BllLot> GetAllLotNameByTime(TimeSpan time);
        IEnumerable<BllLot> FindLotsByCathegoryId(int key);
    }
}
