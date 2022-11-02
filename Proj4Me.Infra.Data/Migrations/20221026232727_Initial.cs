using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proj4Me.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IndexColaboradorProj4Me = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjetoAreaServico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    PerfilId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IndexProjetoProj4Me = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoAreaServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjetoAreaServico_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjetoAreaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IndexClienteProj4Me = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_ProjetoAreaServico_ProjetoAreaServicoId",
                        column: x => x.ProjetoAreaServicoId,
                        principalTable: "ProjetoAreaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjetoAreaServicoColaborador",
                columns: table => new
                {
                    ProjetoAreaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoAreaServicoColaborador", x => new { x.ColaboradorId, x.ProjetoAreaServicoId });
                    table.ForeignKey(
                        name: "FK_ProjetoAreaServicoColaborador_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoAreaServicoColaborador_ProjetoAreaServico_ProjetoAreaServicoId",
                        column: x => x.ProjetoAreaServicoId,
                        principalTable: "ProjetoAreaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndexTarefaProj4Me = table.Column<int>(type: "int", nullable: false),
                    NomeTarefa = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    DataEsforco = table.Column<DateTime>(type: "DateTime", nullable: false),
                    NomeColaborador = table.Column<string>(type: "varchar(300)", nullable: true),
                    Comentario = table.Column<string>(type: "varchar(5000)", nullable: false),
                    TempoGastoDetalhado = table.Column<string>(type: "varchar(100)", nullable: false),
                    TotalTempoGasto = table.Column<string>(type: "varchar(100)", nullable: false),
                    IndexProjetoProj4Me = table.Column<int>(type: "int", nullable: false),
                    ProjetoAreaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefa_ProjetoAreaServico_ProjetoAreaServicoId",
                        column: x => x.ProjetoAreaServicoId,
                        principalTable: "ProjetoAreaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProjetoAreaServicoId",
                table: "Cliente",
                column: "ProjetoAreaServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAreaServico_PerfilId",
                table: "ProjetoAreaServico",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAreaServicoColaborador_ProjetoAreaServicoId",
                table: "ProjetoAreaServicoColaborador",
                column: "ProjetoAreaServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProjetoAreaServicoId",
                table: "Tarefa",
                column: "ProjetoAreaServicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "ProjetoAreaServicoColaborador");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "ProjetoAreaServico");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
