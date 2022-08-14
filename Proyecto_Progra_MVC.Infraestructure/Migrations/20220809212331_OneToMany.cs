using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Progra_MVC.Infraestructure.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Measures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Info",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measures_UserId",
                table: "Measures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Info_UserId",
                table: "Info",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Info_AspNetUsers_UserId",
                table: "Info",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_AspNetUsers_UserId",
                table: "Measures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Info_AspNetUsers_UserId",
                table: "Info");

            migrationBuilder.DropForeignKey(
                name: "FK_Measures_AspNetUsers_UserId",
                table: "Measures");

            migrationBuilder.DropIndex(
                name: "IX_Measures_UserId",
                table: "Measures");

            migrationBuilder.DropIndex(
                name: "IX_Info_UserId",
                table: "Info");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Info");
        }
    }
}
