using AutoMapper;
using BLL.Interface;
using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BllMappers;

namespace BLL.Services
{
    public class MainService:IMainService
    {
        private readonly IUnitOfWork uow;
        private readonly ILotRepository lotRepository;
        private readonly ICathegoryRepository cathegoryRepository;

        public MainService(IUnitOfWork uow, ILotRepository lotrepository, ICathegoryRepository cathegoryRepository)
        {
            this.uow = uow;
            this.lotRepository = lotrepository;
            this.cathegoryRepository = cathegoryRepository;
        }
        public IEnumerable<BllLot> FindAllExposeLots()
        {
            return Maper.ToBllLot(lotRepository.GetAllExposeLots());
        }

        public IEnumerable<BllLot> FindLotsByCathegoryNameAndStatysInProcess(string name)
        {
            return Maper.ToBllLot(cathegoryRepository.GetLotsByCathegoryNameAndStatysInProcess(name));
        }

        public IEnumerable<string> GetAllNames()
        {
            return cathegoryRepository.GetAllNames();
        }

        public IEnumerable<BllLot> FindLotByName(string name)
        {
           return Maper.ToBllLot(lotRepository.GetLotsByName(name));
        }
    }
}
