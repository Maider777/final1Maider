using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "frente",
                columns: table => new
                {
                    IdFrente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrenteIdFrente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frente", x => x.IdFrente);
                    table.ForeignKey(
                        name: "FK_frente_frente_FrenteIdFrente",
                        column: x => x.FrenteIdFrente,
                        principalTable: "frente",
                        principalColumn: "IdFrente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "misil",
                columns: table => new
                {
                    IdMisil = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Megatones = table.Column<float>(type: "real", nullable: false),
                    MisilIdMisil = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_misil", x => x.IdMisil);
                    table.ForeignKey(
                        name: "FK_misil_misil_MisilIdMisil",
                        column: x => x.MisilIdMisil,
                        principalTable: "misil",
                        principalColumn: "IdMisil",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contrato",
                columns: table => new
                {
                    idContrato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    idFrente = table.Column<int>(type: "int", nullable: false),
                    IdMisil = table.Column<int>(type: "int", nullable: false),
                    misilIdMisil = table.Column<int>(type: "int", nullable: true),
                    frenteIdFrente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contrato", x => x.idContrato);
                    table.ForeignKey(
                        name: "FK_contrato_frente_frenteIdFrente",
                        column: x => x.frenteIdFrente,
                        principalTable: "frente",
                        principalColumn: "IdFrente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contrato_misil_misilIdMisil",
                        column: x => x.misilIdMisil,
                        principalTable: "misil",
                        principalColumn: "IdMisil",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contrato_frenteIdFrente",
                table: "contrato",
                column: "frenteIdFrente");

            migrationBuilder.CreateIndex(
                name: "IX_contrato_misilIdMisil",
                table: "contrato",
                column: "misilIdMisil");

            migrationBuilder.CreateIndex(
                name: "IX_frente_FrenteIdFrente",
                table: "frente",
                column: "FrenteIdFrente");

            migrationBuilder.CreateIndex(
                name: "IX_misil_MisilIdMisil",
                table: "misil",
                column: "MisilIdMisil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contrato");

            migrationBuilder.DropTable(
                name: "frente");

            migrationBuilder.DropTable(
                name: "misil");
        }
    }
}
