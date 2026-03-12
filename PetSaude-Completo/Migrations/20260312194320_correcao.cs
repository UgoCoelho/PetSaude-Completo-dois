using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaude_Completo.Migrations
{
    /// <inheritdoc />
    public partial class correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente");

            migrationBuilder.RenameTable(
                name: "Paciente",
                newName: "Pacientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "PacienteId");

            migrationBuilder.CreateTable(
                name: "PacienteComorbidades",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ComorbidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteComorbidades", x => new { x.PacienteId, x.ComorbidadeId });
                    table.ForeignKey(
                        name: "FK_PacienteComorbidades_Comorbidade_ComorbidadeId",
                        column: x => x.ComorbidadeId,
                        principalTable: "Comorbidade",
                        principalColumn: "ComorbidadeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteComorbidades_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteComorbidades_ComorbidadeId",
                table: "PacienteComorbidades",
                column: "ComorbidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacienteComorbidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "Paciente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente",
                column: "PacienteId");
        }
    }
}
