using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.Dictionaries;

namespace StorekeeperAssistant.Infrastructure.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary> Конфигурация сущности <see cref="Product"/> </summary>
        public void Configure(EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("products");

            productConfiguration.Ignore(b => b.DomainEvents);

            productConfiguration.Property(x => x.Id).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired()
                .HasColumnName("id").UseIdentityByDefaultColumn();

            productConfiguration.Property(x => x.Count).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("count");

            productConfiguration.Property<int>("NomenclatureId").UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired().HasColumnName("nomenclature_id");

            productConfiguration.Property<int>("CompanyWarehouseId").UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired().HasColumnName("company_warehouse_id");

            productConfiguration.HasOne(x => x.Nomenclature)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.NomenclatureId);

            productConfiguration.HasOne(x => x.CompanyWarehouse)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.CompanyWarehouseId);
        }
    }
}
