using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Progra_MVC.DataAccess.Migrations
{
    public partial class AddGenderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "AspNetUsers");
        }
    }
}
