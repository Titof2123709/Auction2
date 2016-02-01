using AutoMapper;
using BLL.Interface;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using DAL.Interface.DalModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork uow;
        private readonly ILotRepository lotRepository;

        public LotService(IUnitOfWork uow, ILotRepository repository)
        {
            this.uow = uow;
            this.lotRepository = repository;
            //Debug.WriteLine("service create!");
        }

        public IEnumerable<BllLot> GetAllLotNameByTime(TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BllLot> FindLotsByCathegoryId(int key)
        {
            Mapper.CreateMap<IEnumerable<DalLot>,IEnumerable<BllLot>>();
            return Mapper.Map<IEnumerable<DalLot>,IEnumerable<BllLot>>(lotRepository.GetLotsByCathegoryId(key));
        }

    }
}
