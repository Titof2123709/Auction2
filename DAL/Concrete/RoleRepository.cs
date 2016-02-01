using AutoMapper;
using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using ORM;
using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using DAL.DalMappers;
namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;
        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<string> GetAllNames()
        {
          var roles = context.Set<OrmRole>().Select(dbrole=>dbrole.Name);
          return roles.AsEnumerable();
        }

        public DalRole GetById(int key)
        {
            var ormrole = context.Set<OrmRole>().FirstOrDefaultAsync(dbrole => dbrole.Id == key).Result;
            Mapper.CreateMap<OrmRole, DalRole>();
            return Mapper.Map<OrmRole, DalRole>(ormrole);
        }

        public void Delete(DalRole dalrole)
        {
            throw new NotImplementedException();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole dalrole)
        {
            var role = context.Set<OrmRole>().Where(dbrole => dbrole.Name == dalrole.Name).FirstOrDefault();
            Maper.ToOrmRole(dalrole, role);
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<OrmRole>().AsEnumerable().ToDalRole();
        }


        public void Create(DalRole dalrole)
        {
            context.Set<OrmRole>().Add(dalrole.ToOrmRole());            
        }

        public void Delete(int id)
        {
            context.Set<OrmRole>().Where(dbrole => dbrole.Id == id).Delete();
        }


        public IEnumerable<DalUser> GetUsersByRoleName(string role)
        {
            var users = context.Set<OrmRole>().Where(dbrole => dbrole.Name.Equals(role)).FirstOrDefault().OrmUsers.AsEnumerable();
            return Maper.ToDalUser(users);
        }


        public bool RoleExists(string name)
        {
            return context.Set<OrmRole>().FirstOrDefault(dbrole => dbrole.Name == name) != null;
        }

        //проверить на правильность!!!
        public void AddRoleForUserByUserId(string name,int UserId)
        {
            var role = context.Set<OrmRole>().Where(dbrole => dbrole.Name == name).FirstOrDefault();
            context.Set<OrmUser>().Where(dbuser => dbuser.Id == UserId).FirstOrDefault().OrmRoles.Add(role);
        }


        public void Delete(string rolename)
        {
            context.Set<OrmRole>().Where(dbrole => dbrole.Name == rolename).Delete();
        }


    }
}
