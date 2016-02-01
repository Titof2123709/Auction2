using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Interface.BLLModel;
using DAL.Interface.DalModel;
using DAL.Interface.DalInterface;
using AutoMapper;

//было BLL усли что..я поменял!
namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
            //Debug.WriteLine("service create!");
        }

        public IEnumerable<BllUser> GetAllUserEntities()
        {
            //using (uow)
            {
                Mapper.CreateMap<DalUser, BllUser>();
                return userRepository.GetAll().AsEnumerable().Select(user => Mapper.Map<DalUser, BllUser>(user));
            }
        }

        public BllUser FindById(int key)
        {
            Mapper.CreateMap<DalUser, BllUser>();
            return Mapper.Map<DalUser, BllUser>(userRepository.GetById(key));
        }

        public bool FindByEmailPassword(BllUser bllUser)
        {
            Mapper.CreateMap<BllUser, DalUser>();
            var b = userRepository.GetByPasswordEmail(Mapper.Map<BllUser, DalUser>(bllUser));
            uow.Commit();
            return b;
        }

    }
}
