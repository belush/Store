using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Store.BLL.Interfaces;
using Store.BLL.Logic;
using Store.DAL.Context;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;

namespace Store.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //TODO: try without arg
            //kernel.Bind<IColorLogic>()
            //    .To<ColorLogic>()
            //    .WithConstructorArgument("repository", new ColorRepository(context));

            //kernel.Bind<DbContext>().To<StoreContext>().InSingletonScope();
            //kernel.Bind<DbContext>().To<StoreContext>().

            //kernel.Bind<IGoodLogic>()
            //   .To<GoodLogic>()
            //   .WithConstructorArgument("repository", new ColorRepository(context));

            //ninject constructor injection

            var context = new StoreContext();

            kernel.Bind<IRepository<Color>>().To<ColorRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IRepository<Good>>().To<GoodRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IRepository<Category>>().To<CategoryRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            //kernel.Bind<IRepository<OrderItem>>().To<OrderItemRepository>()
            //    .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IOrderItemRepository>().To<OrderItemRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IRepository<Order>>().To<OrderRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IRepository<Status>>().To<StatusRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IClientRepository>().To<ClientRepository>()
                .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IGoodRepository>().To<GoodRepository>()
             .WithConstructorArgument("storeContext", StoreContext.StoreContextInstance);

            kernel.Bind<IGoodLogic>().To<GoodLogic>();
            kernel.Bind<IColorLogic>().To<ColorLogic>();
            kernel.Bind<ICategoryLogic>().To<CategoryLogic>();
            kernel.Bind<IOrderItemLogic>().To<OrderItemLogic>();
            kernel.Bind<IOrderLogic>().To<OrderLogic>();
            kernel.Bind<IStatusLogic>().To<StatusLogic>();
            kernel.Bind<IClientLogic>().To<ClientLogic>();
        }
    }
}