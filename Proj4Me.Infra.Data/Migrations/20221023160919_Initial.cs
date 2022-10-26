using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proj4Me.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexClienteProj4Me = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IndexColaboradorProj4Me = table.Column<int>(type: "int", nullable: false)
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
                    Index = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_ProjetoAreaServico_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoAreaServico_Colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoAreaServico_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<long>(type: "bigint", nullable: false),
                    NomeTarefa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEsforco = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NomeColaborador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempoGastoDetalhado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTempoGasto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjetoAreaServicoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjetoAreaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CascadeMode = table.Column<int>(type: "int", nullable: false),
                    ClassLevelCascadeMode = table.Column<int>(type: "int", nullable: false),
                    RuleLevelCascadeMode = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Tarefa_ProjetoAreaServico_ProjetoAreaServicoId1",
                        column: x => x.ProjetoAreaServicoId1,
                        principalTable: "ProjetoAreaServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAreaServico_ClienteId",
                table: "ProjetoAreaServico",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAreaServico_ColaboradorId",
                table: "ProjetoAreaServico",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAreaServico_PerfilId",
                table: "ProjetoAreaServico",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProjetoAreaServicoId",
                table: "Tarefa",
                column: "ProjetoAreaServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_ProjetoAreaServicoId1",
                table: "Tarefa",
                column: "ProjetoAreaServicoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "ProjetoAreaServico");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
