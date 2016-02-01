using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using ORM.ORMModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.DalMappers;
using EntityFramework.Extensions;
namespace DAL.Concrete
{
   public class ProfileRepository: IProfileRepository
    {
        private readonly DbContext context;
        public ProfileRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Delete(DalProfile e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalProfile GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalProfile dalprofile)
        {
            context.Set<OrmProfile>().Add(Maper.ToOrmProfile(dalprofile));
        }

        public void Update(DalProfile dalprofile)
        {
          var profile = context.Set<OrmProfile>().Where(dbprof => dbprof.Id == dalprofile.Id).FirstOrDefault();
            Maper.ToOrmProfile(dalprofile,profile);
        }

        public bool UserHasProfile(string name)
        {
            return context.Set<OrmProfile>().Where(dbprofile => dbprofile.OrmUser.Name == name).FirstOrDefault()!=null;
        }

        public DalProfile GetUserProfile(string name)
        {
            return Maper.ToDalProfile(context.Set<OrmProfile>().Where(dbprofile => dbprofile.OrmUser.Name == name).FirstOrDefault());
        }

       //проверить!!
        public DalProfile GetProfileByUserId(int id)
        {         
            var profile = context.Set<OrmProfile>().Where(dbprofile => dbprofile.Id == id).FirstOrDefault();
            return Maper.ToDalProfile(profile);
        }
    }
}
