using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using nmct.ssa.labo2.webshop.Models;
using nmct.ssa.labo2.webshop.Models.DAL.Repositories;
using nmct.ssa.labo2.webshop.Services;

namespace nmct.ssa.labo2.webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IGenericRepository<ProgrammingFramework>, GenericRepository<ProgrammingFramework>>();
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IProductService, ProductService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}