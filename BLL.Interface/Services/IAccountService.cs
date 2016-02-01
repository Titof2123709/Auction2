using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IAccountService : IService
    {
        IEnumerable<string> GetAllNames();
        BllUser GetUserByName(string username);
        bool CheckUserForRole(string username, string role);
        void CreateUser(BllUser blluser);
        bool UserExist(string name);
        bool UserEmailExist(string email);

        //test expression
        void Test();

    }
}
