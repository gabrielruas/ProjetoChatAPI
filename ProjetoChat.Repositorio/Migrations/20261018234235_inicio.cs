using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoChat.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Online = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "atendimentos",
                columns: table => new
                {
                    AtendimentoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCliente = table.Column<string>(type: "TEXT", nullable: false),
                    EmailCliente = table.Column<string>(type: "TEXT", nullable: false),
                    Celular = table.Column<string>(type: "TEXT", nullable: true),
                    Assunto = table.Column<string>(type: "TEXT", nullable: false),
                    FuncionarioID = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atendimentos", x => x.AtendimentoId);
                    table.ForeignKey(
                        name: "FK_atendimentos_usuarios_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mensagens",
                columns: table => new
                {
                    MensagemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AtendimentoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Remetente = table.Column<string>(type: "TEXT", nullable: false),
                    Texto = table.Column<string>(type: "TEXT", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensagens", x => x.MensagemId);
                    table.ForeignKey(
                        name: "FK_mensagens_atendimentos_AtendimentoID",
                        column: x => x.AtendimentoID,
                        principalTable: "atendimentos",
                        principalColumn: "AtendimentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atendimentos_FuncionarioID",
                table: "atendimentos",
                column: "FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_mensagens_AtendimentoID_DataEnvio_MensagemId",
                table: "mensagens",
                columns: new[] { "AtendimentoID", "DataEnvio", "MensagemId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mensagens");

            migrationBuilder.DropTable(
                name: "atendimentos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
