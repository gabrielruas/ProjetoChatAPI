using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoChat.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class Mensagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_mensagens_AtendimentoID_DataEnvio_MensagemId",
                table: "mensagens",
                columns: new[] { "AtendimentoID", "DataEnvio", "MensagemId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mensagens");
        }
    }
}
