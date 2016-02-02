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
using Ninject.Web.Common;
using Ninject;

namespace DependencyResolver
{
        public static class ResolverConfig
        {
            public static void ConfigurateResolverWeb(this IKernel kernel)
            {
                Configure(kernel, true);
            }

            public static void ConfigurateResolverConsole(this IKernel kernel)
            {
                Configure(kernel, false);
            }

            private static void Configure(IKernel kernel, bool isWeb)
            {
                if (isWeb)
                {
                    kernel.Bind<DbContext>().To<EntityModelContext>().InRequestScope();
                    kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                }
                else
                {
                    kernel.Bind<DbContext>().To<EntityModelContext>().InSingletonScope();
                    kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                }

                kernel.Bind<IUserRepository>().To<UserRepository>();
                kernel.Bind<IRoleRepository>().To<RoleRepository>();
                kernel.Bind<ILotRepository>().To<LotRepository>();
                kernel.Bind<ICathegoryRepository>().To<CathegoryRepository>();
                kernel.Bind<IProfileRepository>().To<ProfileRepository>();
                kernel.Bind<ICountryRepository>().To<CountryRepository>();
                kernel.Bind<IImageRepository>().To<ImageRepository>();

                kernel.Bind<IMainService>().To<MainService>();
                kernel.Bind<IAccountService>().To<AccountService>();
                kernel.Bind<ICabinetService>().To<CabinetService>();
                kernel.Bind<IAuctionService>().To<AuctionService>();
                kernel.Bind<IAdminService>().To<AdminService>();
                
            }
        }
    
}
