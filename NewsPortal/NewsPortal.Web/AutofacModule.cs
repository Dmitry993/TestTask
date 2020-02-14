using Autofac;
using Microsoft.AspNetCore.Http;
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

            builder.RegisterType<PostRepository>()
               .As<IPostRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<PostService>()
                .As<IPostService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentRepository>()
                .As<ICommentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentService>()
                .As<ICommentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostRatingRepository>()
                .As<IPostRatingRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<PostRatingService>()
                .As<IPostRatingService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentRatingRepository>()
                .As<ICommentRatingRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CommentRatingService>()
                .As<ICommentRatingService>()
                .InstancePerLifetimeScope();
        }
    }
}
