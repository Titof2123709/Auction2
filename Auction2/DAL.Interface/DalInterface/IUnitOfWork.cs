using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DalInterface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
