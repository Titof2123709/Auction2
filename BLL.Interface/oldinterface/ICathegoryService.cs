using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ICathegoryService
    {
        BllCathegory FindById(int key);
        void Create(BllCathegory e);
        int FindIdByName(string name);

    }
}
