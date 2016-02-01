using AutoMapper;
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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;
        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
            //Debug.WriteLine("service create!");
        }

        public void Create(BllRole role)
        {
            Mapper.CreateMap<BllRole, DalRole>();
            roleRepository.Create(Mapper.Map<BllRole, DalRole>(role));
            uow.Commit();
        }

        public BllRole FindById(int key)
        {
            Mapper.CreateMap<DalRole, BllRole>();
            return Mapper.Map<DalRole, BllRole>(roleRepository.GetById(key));
        }

    }
}
