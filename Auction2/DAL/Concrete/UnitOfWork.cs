using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DalInterface;
using System.Data.Entity;
using ORM;

namespace DAL.Concrete
{
    public class UnitOfWork :Disposable,IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose();
        }

    }
}
