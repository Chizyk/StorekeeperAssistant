// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StorekeeperAssistant.Infrastructure;

namespace StorekeeperAssistant.API.Migrations
{
    [DbContext(typeof(StorekeeperAssistantContext))]
    partial class StorekeeperAssistantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyWarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("company_warehouse_id");

                    b.Property<int>("Count")
                        .HasColumnType("integer")
                        .HasColumnName("count");

                    b.Property<int>("NomenclatureId")
                        .HasColumnType("integer")
                        .HasColumnName("nomenclature_id");

                    b.HasKey("Id");

                    b.HasIndex("CompanyWarehouseId");

                    b.HasIndex("NomenclatureId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.NomenclatureMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Count")
                        .HasColumnType("integer")
                        .HasColumnName("count");

                    b.Property<int>("NomenclatureId")
                        .HasColumnType("integer")
                        .HasColumnName("nomenclature_id");

                    b.Property<int>("ProductMovementId")
                        .HasColumnType("integer")
                        .HasColumnName("product_movement_id");

                    b.HasKey("Id");

                    b.HasIndex("NomenclatureId");

                    b.HasIndex("ProductMovementId");

                    b.ToTable("nomenclature_movements");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.ProductMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AcceptanceCompanyWarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("acceptance_company_warehouse_id");

                    b.Property<string>("MovementStatus")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("movement_status");

                    b.Property<int?>("ShippingCompanyWarehouseId")
                        .HasColumnType("integer")
                        .HasColumnName("shipping_company_warehouse_id");

                    b.Property<DateTime>("_movementDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("movement_date");

                    b.HasKey("Id");

                    b.HasIndex("AcceptanceCompanyWarehouseId");

                    b.HasIndex("ShippingCompanyWarehouseId");

                    b.ToTable("product_movements");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.Dictionaries.CompanyWarehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("company_warehouses");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.Dictionaries.Nomenclature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("nomenclatures");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate.Product", b =>
                {
                    b.HasOne("StorekeeperAssistant.Domain.Dictionaries.CompanyWarehouse", "CompanyWarehouse")
                        .WithMany()
                        .HasForeignKey("CompanyWarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorekeeperAssistant.Domain.Dictionaries.Nomenclature", "Nomenclature")
                        .WithMany()
                        .HasForeignKey("NomenclatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyWarehouse");

                    b.Navigation("Nomenclature");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.NomenclatureMovement", b =>
                {
                    b.HasOne("StorekeeperAssistant.Domain.Dictionaries.Nomenclature", null)
                        .WithMany()
                        .HasForeignKey("NomenclatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.ProductMovement", null)
                        .WithMany("NomenclatureMovements")
                        .HasForeignKey("ProductMovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.ProductMovement", b =>
                {
                    b.HasOne("StorekeeperAssistant.Domain.Dictionaries.CompanyWarehouse", "AcceptanceCompanyWarehouse")
                        .WithMany()
                        .HasForeignKey("AcceptanceCompanyWarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorekeeperAssistant.Domain.Dictionaries.CompanyWarehouse", "ShippingCompanyWarehouse")
                        .WithMany()
                        .HasForeignKey("ShippingCompanyWarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AcceptanceCompanyWarehouse");

                    b.Navigation("ShippingCompanyWarehouse");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate.ProductMovement", b =>
                {
                    b.Navigation("NomenclatureMovements");
                });
#pragma warning restore 612, 618
        }
    }
}
