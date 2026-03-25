using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSaude_Completo.Migrations
{
    /// <inheritdoc />
    public partial class novas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MicroareaId",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeAtendimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeAtendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeAtendimento_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgenteSaude",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadeAtendimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteSaude", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgenteSaude_UnidadeAtendimento_UnidadeAtendimentoId",
                        column: x => x.UnidadeAtendimentoId,
                        principalTable: "UnidadeAtendimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Microarea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadeAtendimentoId = table.Column<int>(type: "int", nullable: false),
                    AgenteSaudeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Microarea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Microarea_AgenteSaude_AgenteSaudeId",
                        column: x => x.AgenteSaudeId,
                        principalTable: "AgenteSaude",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Microarea_UnidadeAtendimento_UnidadeAtendimentoId",
                        column: x => x.UnidadeAtendimentoId,
                        principalTable: "UnidadeAtendimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_MicroareaId",
                table: "Pacientes",
                column: "MicroareaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgenteSaude_UnidadeAtendimentoId",
                table: "AgenteSaude",
                column: "UnidadeAtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Microarea_AgenteSaudeId",
                table: "Microarea",
                column: "AgenteSaudeId",
                unique: true,
                filter: "[AgenteSaudeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Microarea_UnidadeAtendimentoId",
                table: "Microarea",
                column: "UnidadeAtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAtendimento_EnderecoId",
                table: "UnidadeAtendimento",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Microarea_MicroareaId",
                table: "Pacientes",
                column: "MicroareaId",
                principalTable: "Microarea",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Microarea_MicroareaId",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "Microarea");

            migrationBuilder.DropTable(
                name: "AgenteSaude");

            migrationBuilder.DropTable(
                name: "UnidadeAtendimento");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_MicroareaId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "MicroareaId",
                table: "Pacientes");
        }
    }
}
