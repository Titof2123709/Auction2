using DAL.DalMappers;
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
    public class ImageRepository : IImageRepository
    {
        private readonly DbContext context;
        public ImageRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalImage dalimage)
        {
            context.Set<OrmImage>().Add(Maper.ToOrmImage(dalimage));
        }

        public void Delete(DalImage e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalImage GetById(int key)
        {
           return Maper.ToDalImage(context.Set<OrmImage>().FirstOrDefault(dbimage=>dbimage.Id == key));
        }

        public DalImage GetByPredicate(Expression<Func<DalImage, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalImage entity)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<DalImage> GetImagesByLotId(int id)
        {
            return context.Set<OrmImage>().Where(dbimage => dbimage.OrmLotId == id).AsEnumerable().Select(image=>Maper.ToDalImage(image));
        }

        public void Delete(int Id)
        {
            context.Set<OrmImage>().Where(dbimage=>dbimage.Id==Id).Delete();
        }


        public DalImage GetFirstImageByLotId(int Id)
        {
            return Maper.ToDalImage(context.Set<OrmImage>().FirstOrDefault(im=>im.OrmLotId==Id));
        }
    }
}
