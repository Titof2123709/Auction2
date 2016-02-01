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
           return Maper.ToBllUser(userRepository.GetAll());
        }

        public void DeleteUserById(int id)
        {
            userRepository.Delete(id);
            uow.Commit();
        }


        public IEnumerable<BllRole> GetAllUserRoles(int id)
        {
           return Maper.ToBllRole(userRepository.GetAllUserRoles(id));
        }

        public void AddRole(BllRole bllrole)
        {
            roleRepository.Create(bllrole.ToDalRole());
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
            return Maper.ToBllUser(roleRepository.GetUsersByRoleName(role));
        }


        public IEnumerable<BllUser> FindUserByName(string search)
        {
           return Maper.ToBllUser(userRepository.GetUsersByName(search));
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
            return roleRepository.GetAll().ToBllRole();
        }

        public void UpdateRoleForUser(BllRole bllrole)
        {
            roleRepository.Update(bllrole.ToDalRole());
            uow.Commit();
        }


        public bool UserHasRole(string roleName, int userid)
        {
            return userRepository.UserHasRole(roleName, userid);
        }

        public IEnumerable<BllLot> GetAllLots()
        {
           return Maper.ToBllLot(lotRepository.GetAll());
        }


        public IEnumerable<string> GetAllCathegoryNames()
        {
            return cathegoryRepository.GetAllNames();
        }


        public IEnumerable<BllLot> GetLotsByCathegoryName(string cathegory)
        {
            return Maper.ToBllLot(cathegoryRepository.GetLotsByCathegoryName(cathegory));
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
            return Maper.ToBllLot(lotRepository.FindLotsByUserName(search));
        }
        public IEnumerable<BllLot> FindLotByName(string search)
        {
            return Maper.ToBllLot(lotRepository.GetLotsByName(search));
        }

        public void DeleteLotById(int Id)
        {
            lotRepository.Delete(Id);
            //Delete()
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
