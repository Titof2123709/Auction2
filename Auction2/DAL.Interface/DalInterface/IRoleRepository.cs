using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        IEnumerable<string> GetAllNames();
        void Delete(int id);
        IEnumerable<DalUser> GetUsersByRoleName(string role);
        bool RoleExists(string name);
        void AddRoleForUserByUserId(string name, int UserId);
        void Delete(string rolename);

    }
}
