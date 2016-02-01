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
            //Debug.WriteLine("repository create!");
        }

        public IEnumerable<DalUser> GetAll()
        {
            //http://stackoverflow.com/questions/14770941/linq-and-automapper
            //http://www.devtrends.co.uk/blog/stop-using-automapper-in-your-data-access-code
           // Mapper.CreateMap<OrmUser, DalUser>();

            var ormusers = context.Set<OrmUser>().AsEnumerable();
            return Maper.ToDalUser(ormusers);
            //  return  context.Set<ORMUser>().Select(user => Mapper.Map<ORMUser, DalUser>(user);
            /*  return context.Set<ORMUser>().Select(user => new DalUser()
              {
                  Id = user.Id,
                  Name = user.Name,
                  Email = user.Email
              });*/
        }
        
        public DalUser GetById(int key)
        {
            //проверить гасит ли автомаппер интерфейс IQueryable
            var ormuser = context.Set<OrmUser>().FirstOrDefaultAsync(dbuser => dbuser.Id == key).Result;
            Mapper.CreateMap<OrmUser, DalUser>();
            //   .ForMember("Id", opt => opt.MapFrom(src => src.Id))
            //  .ForMember("Name", opt => opt.MapFrom(src => src.Name));
            return Mapper.Map<OrmUser, DalUser>(ormuser);
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        //testexpressionvisitor
        public DalUser GetByPredicateTest(Expression<Func<DalUser,DalUser>> f)
        {
            throw new NotImplementedException();
        }

        public DalUser GetUserByName(string username)
        {
            var ormuser = context.Set<OrmUser>().FirstOrDefault(dbuser => username == dbuser.Name);
            return Maper.ToDalUser(ormuser);
        }
        public bool CheckUserForRole(string username, string role)
        {
            if(context.Set<OrmUser>().Where(db => db.Name == username && db.OrmRoles.Where(dbr => dbr.Name.Equals(role)).FirstOrDefault() != null).FirstOrDefault()!=null)
            {
                return true;
            }
            return false;
        }
        public bool GetByPasswordEmail(DalUser daluser)
        {
            return context.Set<OrmUser>().FirstOrDefaultAsync(dbuser => daluser.Name == dbuser.Name && daluser.Email == dbuser.Email && daluser.Password == dbuser.Password).Result != null;
        }

        public void Create(DalUser daluser)
        {
            //возможно это не хорошо с позиции сильной связки!!!!!
            int roleid=2;
            Mapper.CreateMap<DalUser, OrmUser>();
            context.Set<OrmRole>().Where(db => db.Id == roleid).FirstOrDefault().OrmUsers.Add(Mapper.Map<DalUser, OrmUser>(daluser));
        }

        public void Delete(DalUser daluser)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser daluser)
        {
            var user = context.Set<OrmUser>().Where(dbuser => dbuser.Id == daluser.Id).FirstOrDefault();
            Maper.ToOrmUser(daluser, user);
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
            var roles = context.Set<OrmUser>().Where(dbuser => dbuser.Id == id).FirstOrDefault().OrmRoles.AsEnumerable();
            return Maper.ToDalRole(roles);
        }


        public IEnumerable<DalUser> GetUsersByName(string username)
        {
            var ormuser = context.Set<OrmUser>().Where(dbuser => dbuser.Name == username);
            return Maper.ToDalUser(ormuser);
        }


        public void Delete(int userid, string rolename)
        {
            //проверить как работает!!
           var user = context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Id == userid);
           var role = user.OrmRoles.FirstOrDefault(dbrole => dbrole.Name == rolename);
           role.OrmUsers.Remove(user);
        }

        public bool UserHasRole(string roleName,int userid)
        {
            bool b = context.Set<OrmUser>().FirstOrDefault(dbuser => dbuser.Id== userid).OrmRoles.FirstOrDefault(dbrole => dbrole.Name == roleName) != null;
            return b;
        }


        public DalUser GetUserByLotId(int Id)
        {
            return Maper.ToDalUser(context.Set<OrmLot>().Where(dblot => dblot.Id == Id).FirstOrDefault().OrmUser);
        }
    }
}
