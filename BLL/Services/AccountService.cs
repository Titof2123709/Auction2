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
using BLL.BllMappers;

namespace BLL.Services
{
    public class AccountService:IAccountService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        public AccountService(IUnitOfWork uow, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public bool CheckUserForRole(string username, string role)
        {
            return userRepository.CheckUserForRole(username, role);
        }

        public void CreateUser(BllUser blluser)
        {
            userRepository.Create(blluser.ToDalUser());
            uow.Commit();
        }

        public BllUser GetUserByName(string username)
        {
            return Maper.ToBllUser(userRepository.GetUserByName(username));
        }

        public IEnumerable<string> GetAllNames()
        {
            return roleRepository.GetAllNames();
        }

        //test expressionvisitor
        public void Test()
        {
            userRepository.GetByPredicateTest(x => new DalUser() { Name = x.Name });
        }

        public bool UserEmailExist(string email)
        {
            return userRepository.UserEmailExist(email);
        }

        public bool UserExist(string name)
        {
            return userRepository.UserExist(name);
        }
    }
}
