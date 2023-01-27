using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.Dictionaries;

namespace StorekeeperAssistant.Infrastructure.EntityConfigurations
{
    /// <summary> Конфигурация сущности <see cref="Nomenclature"/> </summary>
    public class NomenclatureEntityTypeConfiguration : IEntityTypeConfiguration<Nomenclature>
    {
        /// <summary> Конфигурация сущности <see cref="Nomenclature"/> </summary>
        public void Configure(EntityTypeBuilder<Nomenclature> nomenclatureConfiguration)
        {
            nomenclatureConfiguration.ToTable("nomenclatures");

            nomenclatureConfiguration.Ignore(b => b.DomainEvents);

            nomenclatureConfiguration.Property(x => x.Id).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired().HasColumnName("id").UseIdentityByDefaultColumn();

            nomenclatureConfiguration.Property(b => b.Name).UsePropertyAccessMode(PropertyAccessMode.Field).HasMaxLength(255).HasColumnName("name");
        }
    }
}
