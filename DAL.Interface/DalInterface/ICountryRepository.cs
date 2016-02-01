using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
   public interface ICountryRepository : IRepository<DalCountry>
    {
       IEnumerable<string> GetAllCountries();
       string GetCountryNameById(int id);
       int GetIdByCountryName(string name);
    }
}
