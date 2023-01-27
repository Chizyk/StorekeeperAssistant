using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Dictionaries;

namespace StorekeeperAssistant.Infrastructure.EntityConfigurations
{
    /// <summary> Конфигурация сущности <see cref="NomenclatureMovement"/> </summary>
    public class NomenclatureMovementEntityTypeConfiguration : IEntityTypeConfiguration<NomenclatureMovement>
    {
        /// <summary> Конфигурация сущности <see cref="NomenclatureMovement"/> </summary>
        public void Configure(EntityTypeBuilder<NomenclatureMovement> nomenclatureMovementConfiguration)
        {
            nomenclatureMovementConfiguration.ToTable("nomenclature_movements");

            nomenclatureMovementConfiguration.Ignore(b => b.DomainEvents);

            nomenclatureMovementConfiguration.Property(x => x.Id).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired()
                .HasColumnName("id").UseIdentityByDefaultColumn();

            nomenclatureMovementConfiguration.Property(x => x.Count).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("count");

            nomenclatureMovementConfiguration.Property(x => x.NomenclatureId).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired()
                .HasColumnName("nomenclature_id");

            nomenclatureMovementConfiguration.Property<int>("ProductMovementId").HasColumnName("product_movement_id")
                .IsRequired();

            nomenclatureMovementConfiguration.HasOne<Nomenclature>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.NomenclatureId);
        }
    }
}
