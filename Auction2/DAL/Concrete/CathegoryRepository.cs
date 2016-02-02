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
using DAL.DalMappers;
namespace DAL.Concrete
{
    public class CathegoryRepository : ICathegoryRepository
    {

        private readonly DbContext context;
        public CathegoryRepository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(DalCathegory e)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalCathegory e)
        {
            throw new NotImplementedException();
        }

        public DalCathegory GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalCathegory GetByPredicate(Expression<Func<DalCathegory, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalCathegory entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalCathegory> GetAll()
        {
            throw new NotImplementedException();
        }



        public IEnumerable<string> GetAllNames()
        {
            return context.Set<OrmCathegory>().Select(db => db.Name);
        }

 
        public int GetIdByName(string name)
        {          
            return context.Set<OrmCathegory>().FirstOrDefault(dbcathegory=>dbcathegory.Name==name).Id;
        }


        public IEnumerable<DalLot> GetLotsByCathegoryNameAndStatysInProcess(string name)
        {           
            return context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Name == name).FirstOrDefault().OrmLots.Where(dblot => dblot.OrmStatysId == 2).AsEnumerable().Select(lot=>Maper.ToDalLot(lot));          
        }


        public IEnumerable<DalLot> GetLotsByCathegoryName(string name)
        {
            return context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Name==name).FirstOrDefault().OrmLots.Select(lot => Maper.ToDalLot(lot));
        }

        public string GetCathegoryNameById(int id)
        {
            return context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Id == id).FirstOrDefault().Name;
        }

    }
}
