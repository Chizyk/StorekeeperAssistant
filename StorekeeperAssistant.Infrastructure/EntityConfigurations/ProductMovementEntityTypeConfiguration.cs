using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Dictionaries;
using System;

namespace StorekeeperAssistant.Infrastructure.EntityConfigurations
{
    public class ProductMovementEntityTypeConfiguration : IEntityTypeConfiguration<ProductMovement>
    {
        /// <summary> Конфигурация сущности <see cref="ProductMovement"/> </summary>
        public void Configure(EntityTypeBuilder<ProductMovement> productMovementConfiguration)
        {
            productMovementConfiguration.ToTable("product_movements");

            productMovementConfiguration.Ignore(b => b.DomainEvents);

            productMovementConfiguration.Property(x => x.Id).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired()
                .HasColumnName("id").UseIdentityByDefaultColumn();

            productMovementConfiguration.Property<int>("AcceptanceCompanyWarehouseId").UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired().HasColumnName("acceptance_company_warehouse_id");

            productMovementConfiguration.Property<int?>("ShippingCompanyWarehouseId").UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false).HasColumnName("shipping_company_warehouse_id");

            var navigation = productMovementConfiguration.Metadata.FindNavigation(nameof(ProductMovement.NomenclatureMovements));
            
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            productMovementConfiguration.Property(x => x.MovementStatus).IsRequired().HasColumnName("movement_status")
                .HasConversion<string>().UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(128);

            productMovementConfiguration
                .Property<DateTime>("_movementDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("movement_date")
                .IsRequired();

            productMovementConfiguration.HasOne(x => x.AcceptanceCompanyWarehouse)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AcceptanceCompanyWarehouseId);

            productMovementConfiguration.HasOne(x => x.ShippingCompanyWarehouse)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(x => x.ShippingCompanyWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
