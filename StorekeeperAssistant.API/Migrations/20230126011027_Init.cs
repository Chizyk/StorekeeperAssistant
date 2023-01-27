using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StorekeeperAssistant.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company_warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nomenclatures",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nomenclatures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movement_status = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    shipping_company_warehouse_id = table.Column<int>(type: "integer", nullable: true),
                    acceptance_company_warehouse_id = table.Column<int>(type: "integer", nullable: false),
                    movement_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_movements", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_movements_company_warehouses_acceptance_company_war~",
                        column: x => x.acceptance_company_warehouse_id,
                        principalTable: "company_warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_movements_company_warehouses_shipping_company_wareh~",
                        column: x => x.shipping_company_warehouse_id,
                        principalTable: "company_warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    count = table.Column<int>(type: "integer", nullable: false),
                    company_warehouse_id = table.Column<int>(type: "integer", nullable: false),
                    nomenclature_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_company_warehouses_company_warehouse_id",
                        column: x => x.company_warehouse_id,
                        principalTable: "company_warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_nomenclatures_nomenclature_id",
                        column: x => x.nomenclature_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nomenclature_movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    count = table.Column<int>(type: "integer", nullable: false),
                    nomenclature_id = table.Column<int>(type: "integer", nullable: false),
                    product_movement_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nomenclature_movements", x => x.id);
                    table.ForeignKey(
                        name: "FK_nomenclature_movements_nomenclatures_nomenclature_id",
                        column: x => x.nomenclature_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nomenclature_movements_product_movements_product_movement_id",
                        column: x => x.product_movement_id,
                        principalTable: "product_movements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nomenclature_movements_nomenclature_id",
                table: "nomenclature_movements",
                column: "nomenclature_id");

            migrationBuilder.CreateIndex(
                name: "IX_nomenclature_movements_product_movement_id",
                table: "nomenclature_movements",
                column: "product_movement_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_movements_acceptance_company_warehouse_id",
                table: "product_movements",
                column: "acceptance_company_warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_movements_shipping_company_warehouse_id",
                table: "product_movements",
                column: "shipping_company_warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_company_warehouse_id",
                table: "products",
                column: "company_warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_nomenclature_id",
                table: "products",
                column: "nomenclature_id");

            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql(@"
                    INSERT INTO company_warehouses(id, name)
                    VALUES
                    (1,'Набу'),
                    (2,'Библиотека'),
                    (3,'Нуменор'),
                    (4,'Свалка'),
                    (5,'Анор Лондо');

                    INSERT INTO nomenclatures(id, name)
                    VALUES
                    (1,'Световой меч'),
                    (2,'Звуковая отвёртка'),
                    (3,'Кольцо власти'),
                    (4,'Мамины бусы'),
                    (5,'Фляга с Эстусом');");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nomenclature_movements");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "product_movements");

            migrationBuilder.DropTable(
                name: "nomenclatures");

            migrationBuilder.DropTable(
                name: "company_warehouses");
        }
    }
}
