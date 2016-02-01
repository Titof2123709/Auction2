using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface IUserRepository : IRepository<DalUser>//Add user repository methods!
    {
        bool GetByPasswordEmail(DalUser daluser);
        DalUser GetUserByName(string username);
        bool CheckUserForRole(string username, string role);
        int GetIdByName(string name);
        DalUser GetUserById(int id);
        bool UserExist(string name);
        bool UserEmailExist(string email);
        DalUser GetByPredicateTest(Expression<Func<DalUser,DalUser>> f);
        void Delete(int id);
        IEnumerable<DalRole> GetAllUserRoles(int id);
        IEnumerable<DalUser> GetUsersByName(string username);
        void Delete(int userid, string rolename);
        bool UserHasRole(string roleName,int userid);
        DalUser GetUserByLotId(int Id);

    }
}
