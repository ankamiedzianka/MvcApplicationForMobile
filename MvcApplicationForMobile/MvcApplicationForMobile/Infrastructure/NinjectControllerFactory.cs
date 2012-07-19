using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Ninject.Modules;
using System.Web.Routing;
using MvcApplicationForMobile.DAL;

namespace MvcApplicationForMobile.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel kernel = new StandardKernel(new MyCustomServices());
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }
 
        private class MyCustomServices : NinjectModule
        {
            public override void Load()
            {
                //repositories & unit of work
                Bind<IUnitOfWork>().To<UnitOfWork>(); ;
            }
        }  
    }
}