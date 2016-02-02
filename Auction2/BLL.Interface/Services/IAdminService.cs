using BLL.Interface.BLLModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IAdminService
    {
        IEnumerable<BllUser> GetAllUsers();
        void DeleteUserById(int id);
        IEnumerable<BllRole> GetAllUserRoles(int id);
        void AddRole(BllRole bllrole);
        void DeleteRoleById(int id);
        IEnumerable<string> GetAllRoleNames();
        IEnumerable<BllUser> GetUsersByRoleName(string role);
        IEnumerable<BllUser> FindUserByName(string search);
        bool RoleExists(string name);
        void UpdateRoleForUser(BllRole bllrole);
        void AddRoleForUserByUserId(string name, int UserId);
        IEnumerable<BllRole> GetAllRoles();
        void DeleteRoleForUserByUserIdAndRoleName(int userid,string rolename);
        void DeleteRoleByName(string rolename);
        bool UserHasRole(string roleName, int userid);
        IEnumerable<BllLot> GetAllLots();
        IEnumerable<string> GetAllCathegoryNames();
        IEnumerable<BllLot> GetLotsByCathegoryName(string cathegory);
        BllProfile GetProfileByUserId(int id);
        string GetCountryForProfile(int CountryId);
        IEnumerable<BllLot> FindLotsByUserName(string search);
        IEnumerable<BllLot> FindLotByName(string search);
        void DeleteLotById(int Id);
        BllUser GetUserByLotId(int Id);
        string GetCathegoryNameById(int id);
    }
}
