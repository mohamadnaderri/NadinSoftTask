using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;

namespace NadinSoftTask.Configuration
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Container { get; private set; }
        public static void WireUp(IServiceCollection serviceCollection)
        {
            Container = new WindsorContainer();


        }
 
    }
}