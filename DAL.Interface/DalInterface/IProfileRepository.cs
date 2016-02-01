using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface IProfileRepository: IRepository<DalProfile>
    {
        DalProfile GetUserProfile(string name);
        bool UserHasProfile(string name);
        DalProfile GetProfileByUserId(int id);
    }
}
