using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlujoApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampoCatalogos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Requerido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampoCatalogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatoUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlujoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PasoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CampoCodigo = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flujos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flujos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pasos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    FlujoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pasos_Flujos_FlujoId",
                        column: x => x.FlujoId,
                        principalTable: "Flujos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PasoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampoCodigo = table.Column<string>(type: "text", nullable: false),
                    CampoCatalogoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EsEntrada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campos_CampoCatalogos_CampoCatalogoId",
                        column: x => x.CampoCatalogoId,
                        principalTable: "CampoCatalogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campos_Pasos_PasoId",
                        column: x => x.PasoId,
                        principalTable: "Pasos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasoDependencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PasoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DependeDePasoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasoDependencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasoDependencias_Pasos_PasoId",
                        column: x => x.PasoId,
                        principalTable: "Pasos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campos_CampoCatalogoId",
                table: "Campos",
                column: "CampoCatalogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campos_PasoId",
                table: "Campos",
                column: "PasoId");

            migrationBuilder.CreateIndex(
                name: "IX_PasoDependencias_PasoId",
                table: "PasoDependencias",
                column: "PasoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasos_FlujoId",
                table: "Pasos",
                column: "FlujoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campos");

            migrationBuilder.DropTable(
                name: "DatoUsuarios");

            migrationBuilder.DropTable(
                name: "PasoDependencias");

            migrationBuilder.DropTable(
                name: "CampoCatalogos");

            migrationBuilder.DropTable(
                name: "Pasos");

            migrationBuilder.DropTable(
                name: "Flujos");
        }
    }
}
