using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DalInterface;
using ORM.ORMModels;
using DAL.Interface.DalModel;
using AutoMapper;
using System.Linq.Expressions;
using DAL.DalMappers;
using EntityFramework.Extensions;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }
        public void Delete(DalUser daluser)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<OrmUser>().AsEnumerable().Select(ormuser=>Maper.ToDalUser(ormuser));
        }
        
        public DalUser GetById(int key)
        {
            return Maper.ToDalUser(context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Id == key));
        }

        public DalUser GetUserByName(string username)
        {
            return Maper.ToDalUser(context.Set<OrmUser>().FirstOrDefault(dbuser => username == dbuser.Name));
        }
        public bool CheckUserForRole(string username, string role)
        {
            return context.Set<OrmUser>().Where(db => db.Name == username && db.OrmRoles.Where(dbr => dbr.Name.Equals(role)).FirstOrDefault() != null).FirstOrDefault() != null ? true : false;           
        }
        public bool GetByPasswordEmail(DalUser daluser)
        {
            return context.Set<OrmUser>().FirstOrDefault(dbuser => daluser.Name == dbuser.Name && daluser.Email == dbuser.Email && daluser.Password == dbuser.Password) != null;
        }

        public void Create(DalUser daluser)
        {
            int roleid=2;
            context.Set<OrmRole>().Where(db => db.Id == roleid).FirstOrDefault().OrmUsers.Add(Maper.ToOrmUser(daluser));
        }

        public void Update(DalUser daluser)
        {
            Maper.ToOrmUser(daluser, context.Set<OrmUser>().Where(dbuser => dbuser.Id == daluser.Id).FirstOrDefault());
        }


        public bool UserExist(string name)
        {
            return context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Name.Equals(name))!=null;
        }
        public bool UserEmailExist(string email)
        {
            return context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Email.Equals(email)) != null;
        }

        public int GetIdByName(string name)
        {
            return context.Set<OrmUser>().Where(dbuser => dbuser.Name.Equals(name)).Select(dbuser => dbuser.Id).FirstOrDefault();
        }

        public DalUser GetUserById(int id)
        {
            return Maper.ToDalUser(context.Set<OrmUser>().Where(dbuser => dbuser.Id == id).FirstOrDefault());
        }

        public void Delete(int id)
        {
            context.Set<OrmUser>().Where(dbuser => dbuser.Id == id).Delete();        
        }

        public IEnumerable<DalRole> GetAllUserRoles(int id)
        {
            return context.Set<OrmUser>().Where(dbuser => dbuser.Id == id).FirstOrDefault().OrmRoles.AsEnumerable().Select(role=>Maper.ToDalRole(role));
        }


        public IEnumerable<DalUser> GetUsersByName(string username)
        {
            return context.Set<OrmUser>().Where(dbuser => dbuser.Name == username).AsEnumerable().Select(user => Maper.ToDalUser(user));
        }


        public void Delete(int userid, string rolename)
        {
           var user = context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Id == userid);
           var role = user.OrmRoles.FirstOrDefault(dbrole => dbrole.Name == rolename);
           role.OrmUsers.Remove(user);
        }

        public bool UserHasRole(string roleName,int userid)
        {
            return context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Id== userid).OrmRoles.FirstOrDefault(dbrole => dbrole.Name == roleName) != null;
        }


        public DalUser GetUserByLotId(int Id)
        {
            return Maper.ToDalUser(context.Set<OrmLot>().Where(dblot => dblot.Id == Id).FirstOrDefault().OrmUser);
        }
    }
}
