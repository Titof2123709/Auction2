using AutoMapper;
using BLL.Interface;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
  public class CathegoryService: ICathegoryService
    {
        private readonly IUnitOfWork uow;
        private readonly ICathegoryRepository cathegoryRepository;
        public CathegoryService(IUnitOfWork uow, ICathegoryRepository repository)
        {
            this.uow = uow;
            this.cathegoryRepository = repository;
            //Debug.WriteLine("service create!");
        }

        public BllCathegory FindById(int key)
        {
            Mapper.CreateMap<DalCathegory, BllCathegory>();
            return Mapper.Map<DalCathegory, BllCathegory>(cathegoryRepository.GetById(key));
        }

        public void Create(BllCathegory cathegory)
        {
            Mapper.CreateMap<BllCathegory, DalCathegory>();
            cathegoryRepository.Create(Mapper.Map<BllCathegory, DalCathegory>(cathegory));
            uow.Commit();
        }

        public int FindIdByName(string name)
        {
           return cathegoryRepository.GetIdByName(name);
        }
    }
}
