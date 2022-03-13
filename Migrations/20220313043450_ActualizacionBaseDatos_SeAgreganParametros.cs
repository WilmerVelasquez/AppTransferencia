using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTransferencia.Migrations
{
    public partial class ActualizacionBaseDatos_SeAgreganParametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Banco = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    Apellidos = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    GMF = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Tipo_Tran = table.Column<string>(unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransaccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Banco = table.Column<int>(nullable: false),
                    Saldo = table.Column<decimal>(type: "money", nullable: false),
                    Id_Cliente = table.Column<int>(nullable: false),
                    Num_Cuenta = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Bancos",
                        column: x => x.Id_Banco,
                        principalTable: "Bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes",
                        column: x => x.Id_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Tipo_Transaccion = table.Column<int>(nullable: false),
                    Valor_GMF = table.Column<decimal>(type: "money", nullable: false),
                    Id_Cliente = table.Column<int>(nullable: false),
                    Valor_Retiro = table.Column<decimal>(type: "money", nullable: false),
                    Fecha_Transacción = table.Column<DateTime>(type: "datetime", nullable: false),
                    Cuenta_Origen = table.Column<int>(nullable: false),
                    Cuenta_Destino = table.Column<int>(nullable: false),
                    CuentaDestinoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuentas_CuentaDestinoId",
                        column: x => x.CuentaDestinoId,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Clientes",
                        column: x => x.Id_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuentas1",
                        column: x => x.Cuenta_Origen,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_TipoTransaccion",
                        column: x => x.Id_Tipo_Transaccion,
                        principalTable: "TipoTransaccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_Id_Banco",
                table: "Cuentas",
                column: "Id_Banco");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_Id_Cliente",
                table: "Cuentas",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CuentaDestinoId",
                table: "Transaccion",
                column: "CuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_Id_Cliente",
                table: "Transaccion",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_Cuenta_Origen",
                table: "Transaccion",
                column: "Cuenta_Origen");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_Id_Tipo_Transaccion",
                table: "Transaccion",
                column: "Id_Tipo_Transaccion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");

            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
