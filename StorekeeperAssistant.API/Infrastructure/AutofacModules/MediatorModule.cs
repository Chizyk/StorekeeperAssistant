using System.Reflection;
using Autofac;
using MediatR;
using StorekeeperAssistant.API.Application.Commands;
using StorekeeperAssistant.API.Application.DomainEventHandlers;

namespace StorekeeperAssistant.API.Infrastructure.AutofacModules
{
    /// <summary> Регистрация зависимостей </summary>
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateProductMovementCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(ProductMovementStartDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

        }
    }
}
