using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NadinSoftTask.DomainModel.Product.Interfaces;
using NadinSoftTask.Infrastructure;
using NadinSoftTask.Infrastructure.Persistence.Read.Repositories;
using NadinSoftTask.Infrastructure.Persistence.Write;
using NadinSoftTask.Infrastructure.Persistence.Write.Repositories;

namespace NadinSoftTask.Configuration
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Container { get; private set; }
        public static void WireUp()
        {
            Container = new WindsorContainer();

            Container.Register(Component.For<IProductReadOnlyRepository>().ImplementedBy<ProductReadOlyRepository>().LifestyleScoped());
            Container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>().LifestyleScoped());
            Container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleScoped());
        }

    }
}