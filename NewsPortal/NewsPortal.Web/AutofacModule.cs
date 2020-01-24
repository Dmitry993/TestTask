using Autofac;
using NewsPortal.Data.Model;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Services;

namespace NewsPortal.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();
        }
    }
}
