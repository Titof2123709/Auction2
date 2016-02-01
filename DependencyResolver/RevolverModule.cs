using BLL.Interface.Services;
using DAL.Interface.DalInterface;
using Ninject.Modules;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete;
using BLL;
using BLL.Services;
using BLL.Interface;

namespace DependencyResolver
{
    public class RevolverModule : NinjectModule
    {
        public override void Load()
        {
            //редактировать все это!!!!!!!
            //эти два должны быть InRequestScope() - но чет не получается..не видит...
            Bind<DbContext>().To<EntityModelContext>().InSingletonScope();
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ILotRepository>().To<LotRepository>();
            Bind<ICathegoryRepository>().To<CathegoryRepository>();
            Bind<IProfileRepository>().To<ProfileRepository>();
            Bind<ICountryRepository>().To<CountryRepository>();
            Bind<IImageRepository>().To<ImageRepository>();

            Bind<IMainService>().To<MainService>();
            Bind<IAccountService>().To<AccountService>();
            Bind<ICabinetService>().To<CabinetService>();
            Bind<IAuctionService>().To<AuctionService>();
            Bind<IAdminService>().To<AdminService>();
            
        }
    }
}
