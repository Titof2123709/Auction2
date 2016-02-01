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
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
            //Debug.WriteLine("unit of work create!");
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        /// здесь обдумать необходимость боллее правильно модели для dispose!паттерна dispose!!!
        public void Dispose()
        {
            Dispose(true);
            //Debug.WriteLine("Context dispose!");
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        //вот на эту!!
        /*   private bool disposed = false;

           public virtual void Dispose(bool disposing)
           {
               if (!this.disposed)
               {
                   if (disposing)
                   {
                       db.Dispose();
                   }
               }
               this.disposed = true;
           }

           public void Dispose()
           {
               Dispose(true);
               GC.SuppressFinalize(this);
           }*/
    }
}
