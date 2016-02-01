using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.BLLModel;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        void Create(BllRole role);
        BllRole FindById(int key);
    }
}
