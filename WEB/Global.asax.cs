using BLL.Interface;
using BLL.Interface.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WEB.App_Start;

namespace WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //это то что должно быть в class NinjectWebCommon 
            // private static void RegisterServices(IKernel kernel)
            //перенесено сюда
            //мы наследуем System.Web.HttpApplication здесь благодяря этому
            //такое перенесение стало возможным!
            //чтобы испоользовать проект DependencyResolver класс RevolverModule
            //метод load выше мы пишем AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.DependencyResolver.SetResolver(new WEB.Infrastuctura.NinjectDependencyResolver());
        }

    }
}


/*Этот класс NinjectWebCommon добавляется в проект автоматически при добавлении пакетов Ninject через Nuget. Он как раз и создан для упрощения работы, и, как правило, часто через него идет внедрение зависимостей. Но это не аксиома. Есть различные способы и модификации. Ну например, у нас есть класс NinjectDependencyResolver пусть даже без метода AddBindings(), который сопоставляет типы

Тогда мы можем даже не обращать внимание на класс NinjectCommonWeb. Сделать себе модуль со всеми зависимостями:
public class NinjectRegistrations : NinjectModule
{
public override void Load()
{
Bind<irepository>().To<bookrepository>();
}
}

И потом задействовать его в файле Global.asax:

protected void Application_Start()
{
AreaRegistration.RegisterAllAreas();
FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
RouteConfig.RegisterRoutes(RouteTable.Routes);
BundleConfig.RegisterBundles(BundleTable.Bundles);

// внедрение зависимостей
NinjectModule registrations = new NinjectRegistrations();
var kernel = new StandardKernel(registrations);
DependencyResolver.SetResolver( new NinjectDependencyResolver(kernel));
}

есть еще способ перенести часть кода из NinjectCommonWeb в Global.asax, 
но тогда класс в Global.asax должен наследоваться не от HttpApplication, 
 а от NinjectHttpApplication
*/