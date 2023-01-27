using Autofac;
using StorekeeperAssistant.API.Application.Queries.CompanyWarehouse;
using StorekeeperAssistant.API.Application.Queries.Nomenclature;
using StorekeeperAssistant.API.Application.Queries.ProductMovement;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Infrastructure.Repositories;

namespace StorekeeperAssistant.API.Infrastructure.AutofacModules
{
    /// <summary> Регистрация зависимостей </summary>
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new NomenclatureQueries(QueriesConnectionString))
                .As<INomenclatureQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new CompanyWarehouseQueries(QueriesConnectionString))
                .As<ICompanyWarehouseQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ProductMovementQueries(QueriesConnectionString))
                .As<IProductMovementQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductMovementRepository>()
                .As<IProductMovementRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
