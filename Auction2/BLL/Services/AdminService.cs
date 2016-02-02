using BLL.BllMappers;
using BLL.Interface;
using BLL.Interface.BLLModel;
using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ILotRepository lotRepository;
        private readonly ICathegoryRepository cathegoryRepository;
        private readonly IProfileRepository profileRepository;
        private readonly ICountryRepository countryRepository;

        public AdminService(IUnitOfWork uow, IUserRepository userRepository, IRoleRepository roleRepository, ILotRepository lotRepository, ICathegoryRepository cathegoryRepository, IProfileRepository profileRepository, ICountryRepository countryRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.lotRepository = lotRepository;
            this.cathegoryRepository = cathegoryRepository;
            this.profileRepository = profileRepository;
            this.countryRepository = countryRepository;
        }



        public IEnumerable<BllUser> GetAllUsers()
        {
           return userRepository.GetAll().Select(user=>Maper.ToBllUser(user));
        }

        public void DeleteUserById(int id)
        {
            userRepository.Delete(id);
            uow.Commit();
        }


        public IEnumerable<BllRole> GetAllUserRoles(int id)
        {
           return userRepository.GetAllUserRoles(id).Select(role=>Maper.ToBllRole(role));
        }

        public void AddRole(BllRole bllrole)
        {
            roleRepository.Create(Maper.ToDalRole(bllrole));
            uow.Commit();
        }

        public void DeleteRoleForUserByUserIdAndRoleName(int userid,string rolename)
        {
            userRepository.Delete(userid,rolename);
            uow.Commit();
        }


        public void DeleteRoleById(int id)
        {
            roleRepository.Delete(id);
            uow.Commit();
        }

        public void DeleteRoleByName(string rolename)
        {
            roleRepository.Delete(rolename);
            uow.Commit();
        }
        public IEnumerable<string> GetAllRoleNames()
        {
           return roleRepository.GetAllNames();
        }


        public IEnumerable<BllUser> GetUsersByRoleName(string role)
        {
            return roleRepository.GetUsersByRoleName(role).Select(user=>Maper.ToBllUser(user));
        }


        public IEnumerable<BllUser> FindUserByName(string search)
        {
           return userRepository.GetUsersByName(search).Select(user=>Maper.ToBllUser(user));
        }

        public bool RoleExists(string name)
        {
           return roleRepository.RoleExists(name);
        }


        public void AddRoleForUserByUserId(string name, int UserId)
        {
            roleRepository.AddRoleForUserByUserId(name, UserId);
            uow.Commit();
        }

        public IEnumerable<BllRole> GetAllRoles()
        {
            return roleRepository.GetAll().Select(role=>Maper.ToBllRole(role));
        }

        public void UpdateRoleForUser(BllRole bllrole)
        {
            roleRepository.Update(Maper.ToDalRole(bllrole));
            uow.Commit();
        }


        public bool UserHasRole(string roleName, int userid)
        {
            return userRepository.UserHasRole(roleName, userid);
        }

        public IEnumerable<BllLot> GetAllLots()
        {
           return lotRepository.GetAll().Select(lot=>Maper.ToBllLot(lot));
        }


        public IEnumerable<string> GetAllCathegoryNames()
        {
            return cathegoryRepository.GetAllNames();
        }


        public IEnumerable<BllLot> GetLotsByCathegoryName(string cathegory)
        {
            return cathegoryRepository.GetLotsByCathegoryName(cathegory).Select(lot=>Maper.ToBllLot(lot));
        }


        public BllProfile GetProfileByUserId(int id)
        {
           return Maper.ToBllProfile(profileRepository.GetProfileByUserId(id));
        }


        public string GetCountryForProfile(int CountryId)
        {
           return countryRepository.GetCountryNameById(CountryId);
        }


        public IEnumerable<BllLot> FindLotsByUserName(string search)
        {
            return lotRepository.FindLotsByUserName(search).Select(lot=>Maper.ToBllLot(lot));
        }

        public IEnumerable<BllLot> FindLotByName(string search)
        {
            return lotRepository.GetLotsByName(search).Select(lot=>Maper.ToBllLot(lot));
        }

        public void DeleteLotById(int Id)
        {
            lotRepository.Delete(Id);
            uow.Commit();
        }


        public BllUser GetUserByLotId(int Id)
        {
            return Maper.ToBllUser(userRepository.GetUserByLotId(Id));
        }


        public string GetCathegoryNameById(int id)
        {
            return cathegoryRepository.GetCathegoryNameById(id);
        }
    }
}
