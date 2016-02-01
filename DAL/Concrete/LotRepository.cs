using AutoMapper;
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
using EntityFramework.Extensions;
using EntityFramework.Mapping;
using DAL.DalMappers;

namespace DAL.Concrete
{
    public class LotRepository : ILotRepository
    {
        private readonly DbContext context;
        public LotRepository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(DalLot dallot)
        {
            context.Set<OrmLot>().Add(dallot.ToOrmLot());
        }

        public void Update(DalLot dallot)
        { 
            Mapper.CreateMap<DalLot, OrmLot>();
            OrmLot ormLot = context.Set<OrmLot>().FirstOrDefaultAsync(dbid => dbid.Id == dallot.Id).Result;
            Mapper.Map<DalLot, OrmLot>(dallot, ormLot);
           // context.Set<OrmLot>().Where(dbid => dbid.Id == dallot.Id).Update(dblot => Mapper.Map<DalLot, OrmLot>(dallot, ormLot));
        }

        public void Delete(string name)
        {
            context.Set<OrmLot>().Where(dblot => dblot.Name.Equals(name)).Delete();
        }



        public void Delete(DalLot dallot)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DalLot> GetAll()
        {
            return Maper.ToDalLot(context.Set<OrmLot>());
        }

        public DalLot GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalLot GetByPredicate(Expression<Func<DalLot, bool>> f)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAllLotNameByUserId(int id)
        {
            var ormuser = context.Set<OrmLot>().Where(dblot => dblot.OrmUserId == id).Select(dblot => dblot.Name);
            return ormuser.AsEnumerable();
        }

        public IEnumerable<DalLot> GetAllLotNameByTime(TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public DalLot GetLotByName(string name)
        {
            Mapper.CreateMap<OrmLot, DalLot>();
            return Mapper.Map<OrmLot, DalLot>(context.Set<OrmLot>().FirstOrDefaultAsync(dbuser => dbuser.Name==name).Result);
        }




        public IEnumerable<DalLot> GetLotsByCathegoryId(int key)
        {
            Mapper.CreateMap<IEnumerable<OrmLot>, IEnumerable<DalLot>>();
            IEnumerable<OrmLot> ormlots = context.Set<OrmLot>().Where(dblot => dblot.OrmCathegoryId == key).Select(dblot => dblot);
            //проверить надобность tolist() здесь как условие разрыва с бд!-все ок! разрыв существует проис кеширование!и не влияет на iqertyable! 
            return  Mapper.Map<IEnumerable<OrmLot>, IEnumerable<DalLot>>(ormlots.ToList());
        }


        public IEnumerable<DalLot> GetAllExposeLots()
        {
            //в запрос нельзя вставлять числа наживую!!!
           int key= 2;
            Mapper.CreateMap<OrmLot, DalLot>();
            var ormlots = context.Set<OrmLot>().Where(dblot => dblot.OrmStatysId == key).Select(dblot => dblot);
            return Mapper.Map<IEnumerable<OrmLot>, IEnumerable<DalLot>>(ormlots);
        }


        public IEnumerable<DalLot> GetLotsByName(string name)
        {
           return Maper.ToDalLot(context.Set<OrmLot>().Where(dblot => dblot.Name == name));
        }

        public IEnumerable<DalLot> FindLotsByUserName(string name)
        {
            var ormlot = context.Set<OrmLot>().Where(dblot => dblot.OrmUser.Name == name);
           // var ormlot = context.Set<OrmUser>().Where(dbuser => dbuser.Name == name).FirstOrDefault().OrmLots.AsEnumerable();
            return Maper.ToDalLot(ormlot);
        }

        public void Delete(int Id)
        {
            context.Set<OrmLot>().Where(dblot => dblot.Id==Id).Delete();
        }

    }
}
