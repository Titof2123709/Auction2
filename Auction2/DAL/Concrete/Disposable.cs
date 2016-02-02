using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public abstract class Disposable : IDisposable
    {
         bool disposed=false;
        ~Disposable()
        {
            if(!disposed)
            {
                disposed = true;
                Dispose(false);
            }
        }
        public void Dispose()
        {
            if(!disposed)
            {
                disposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
        public void Close()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing){ }
        }    
    }
}
