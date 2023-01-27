using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.Dictionaries;

namespace StorekeeperAssistant.Infrastructure.EntityConfigurations
{
    /// <summary> Конфигурация сущности <see cref="CompanyWarehouse"/> </summary>
    public class CompanyWarehouseEntityTypeConfiguration : IEntityTypeConfiguration<CompanyWarehouse>
    {
        /// <summary> Конфигурация сущности <see cref="CompanyWarehouse"/> </summary>
        public void Configure(EntityTypeBuilder<CompanyWarehouse> companyWarehouseConfiguration)
        {
            companyWarehouseConfiguration.ToTable("company_warehouses");

            companyWarehouseConfiguration.Ignore(b => b.DomainEvents);

            companyWarehouseConfiguration.Property(x => x.Id).IsRequired().HasColumnName("id").UseIdentityByDefaultColumn();

            companyWarehouseConfiguration.Property(b => b.Name).HasMaxLength(255).HasColumnName("name");
        }
    }
}
