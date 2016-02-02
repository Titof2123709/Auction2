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

        public void Delete(DalLot dallot)
        {
            throw new NotImplementedException();
        }
        public DalLot GetByPredicate(Expression<Func<DalLot, bool>> f)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DalLot> GetAllLotNameByTime(TimeSpan time)
        {
            throw new NotImplementedException();
        }


        public void Create(DalLot dallot)
        {
            context.Set<OrmLot>().Add(dallot.ToOrmLot());
        }

        public void Update(DalLot dallot)
        {
            Maper.ToOrmLot(dallot, context.Set<OrmLot>().FirstOrDefault(dblot => dblot.Id == dallot.Id));
        }

        public void Delete(string name)
        {
            context.Set<OrmLot>().Where(dblot => dblot.Name == name).Delete();
        }

        public IEnumerable<DalLot> GetAll()
        {
            return context.Set<OrmLot>().AsEnumerable().Select(dblot=>Maper.ToDalLot(dblot));
        }

        public DalLot GetById(int key)
        {
            return Maper.ToDalLot(context.Set<OrmLot>().FirstOrDefault(dblot=>dblot.Id==key));
        }

        public IEnumerable<string> GetAllLotNameByUserId(int id)
        {
            return context.Set<OrmLot>().Where(dblot => dblot.OrmUserId == id).Select(dblot => dblot.Name).AsEnumerable();
        }

        public DalLot GetLotByName(string name)
        {
            return Maper.ToDalLot(context.Set<OrmLot>().FirstOrDefault(dblot => dblot.Name==name));
        }

        public IEnumerable<DalLot> GetLotsByCathegoryId(int key)
        {
            return context.Set<OrmLot>().Where(dblot => dblot.OrmCathegoryId == key).Select(dblot => dblot).AsEnumerable().Select(lot=>Maper.ToDalLot(lot));
        }


        public IEnumerable<DalLot> GetAllExposeLots()
        {
            int key= 2;
            return context.Set<OrmLot>().Where(dblot => dblot.OrmStatysId == key).Select(dblot => dblot).AsEnumerable().Select(lot => Maper.ToDalLot(lot));
        }


        public IEnumerable<DalLot> GetLotsByName(string name)
        {
           return context.Set<OrmLot>().Where(dblot => dblot.Name == name).AsEnumerable().Select(dblot=>Maper.ToDalLot(dblot));
        }

        public IEnumerable<DalLot> FindLotsByUserName(string name)
        {
            return context.Set<OrmLot>().Where(dblot => dblot.OrmUser.Name == name).AsEnumerable().Select(dblot => Maper.ToDalLot(dblot));
        }

        public void Delete(int Id)
        {
            context.Set<OrmLot>().Where(dblot => dblot.Id==Id).Delete();
        }

        public int GetLotIdByName(string name)
        {
            return context.Set<OrmLot>().Where(dblot => dblot.Name == name).Select(dblot=>dblot.Id).FirstOrDefault();
        }
    }
}
