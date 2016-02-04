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
using BLL.Interface.BLLModel;

namespace BLL.Services
{
    public class MainService:IMainService
    {
        private readonly IUnitOfWork uow;
        private readonly ILotRepository lotRepository;
        private readonly ICathegoryRepository cathegoryRepository;
        private readonly IImageRepository imageRepository;

        public MainService(IUnitOfWork uow, ILotRepository lotrepository, ICathegoryRepository cathegoryRepository, IImageRepository imageRepository)
        {
            this.uow = uow;
            this.lotRepository = lotrepository;
            this.cathegoryRepository = cathegoryRepository;
            this.imageRepository = imageRepository;
        }
        public IEnumerable<BllLot> FindAllExposeLots()
        {
            return lotRepository.GetAllExposeLots().Select(lot=>Maper.ToBllLot(lot));
        }

        public IEnumerable<BllLot> FindLotsByCathegoryNameAndStatysInProcess(string name)
        {
            return cathegoryRepository.GetLotsByCathegoryNameAndStatysInProcess(name).Select(lot=>Maper.ToBllLot(lot));
        }

        public IEnumerable<string> GetAllNames()
        {
            return cathegoryRepository.GetAllNames();
        }

        public IEnumerable<BllLot> FindLotByName(string name)
        {
           return lotRepository.GetLotsByName(name).Select(lot=>Maper.ToBllLot(lot));
        }


        public BllImage GetFirstImageByLotId(int Id)
        {
            return Maper.ToBllImage(imageRepository.GetFirstImageByLotId(Id));
        }
    }
}
