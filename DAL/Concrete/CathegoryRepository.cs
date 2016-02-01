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

        public IEnumerable<string> GetAllNames()
        {
            var cathegories = context.Set<OrmCathegory>().Select(db => db.Name);
            return cathegories;
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

        //ЭТО УЖЕ НЕ НУЖНО!!Проверить всю линию!
        public int GetIdByName(string name)
        {          
            //здесь посм возможно возврата не целого объекта сразу а только id
            return context.Set<OrmCathegory>().FirstOrDefaultAsync(dbcathegory=>dbcathegory.Name==name).Result.Id;
        }


        public IEnumerable<DalLot> GetLotsByCathegoryNameAndStatysInProcess(string name)
        {           
            //AsEnumerable 
            //http://professorweb.ru/my/LINQ/base/level2/2_10.php
            //виды загрузок(прямая,ленивая,явная)!!!
            //http://professorweb.ru/my/entity-framework/6/level3/3_4.php

            var lots = context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Name == name).FirstOrDefault().OrmLots.Where(dblot => dblot.OrmStatysId == 2).AsEnumerable();
            return Maper.ToDalLot(lots);
            
        }


        public IEnumerable<DalLot> GetLotsByCathegoryName(string name)
        {
            var lots = context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Name==name).FirstOrDefault().OrmLots;
            return Maper.ToDalLot(lots);
        }


        public IEnumerable<DalCathegory> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetCathegoryNameById(int id)
        {
            return context.Set<OrmCathegory>().Where(dbcathegory => dbcathegory.Id == id).FirstOrDefault().Name;
        }

    }
}
