using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<BllUser> GetAllUserEntities();
        BllUser FindById(int key);

        bool FindByEmailPassword(BllUser bllUser);


    }
}
