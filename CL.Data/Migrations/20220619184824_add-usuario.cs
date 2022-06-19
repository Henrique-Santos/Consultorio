using Microsoft.EntityFrameworkCore.Migrations;

namespace CL.Data.Migrations
{
    public partial class addusuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Telefones");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Telefones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CEP",
                table: "Enderecos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones",
                columns: new[] { "ClienteId", "Numero" });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Login);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Telefones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Telefones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones",
                columns: new[] { "ClientId", "Numero" });

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
