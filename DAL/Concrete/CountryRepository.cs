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
namespace DAL.Concrete
{
    public class CountryRepository : ICountryRepository
    {

        private readonly DbContext context;
        public CountryRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalCountry e)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalCountry e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalCountry> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalCountry GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalCountry GetByPredicate(Expression<Func<DalCountry, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalCountry entity)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<string> GetAllCountries()
        {
            var countries = context.Set<OrmCountry>().Select(dbcountry => dbcountry.Name);
            return countries.AsEnumerable();
        }


        public string GetCountryNameById(int id)
        {
            return context.Set<OrmCountry>().Where(dbcountry => dbcountry.Id == id).Select(dbcountry => dbcountry.Name).FirstOrDefault();
        }


        public int GetIdByCountryName(string name)
        {
            return context.Set<OrmCountry>().Where(dbcountry => dbcountry.Name == name).Select(dbcountry => dbcountry.Id).FirstOrDefault();
        }
    }
}
