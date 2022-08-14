using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Progra_MVC.Infraestructure.Migrations
{
    public partial class OneToOneCaloriesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCalories",
                table: "Calories",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Calories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calories_UserId",
                table: "Calories",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Calories_AspNetUsers_UserId",
                table: "Calories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calories_AspNetUsers_UserId",
                table: "Calories");

            migrationBuilder.DropIndex(
                name: "IX_Calories_UserId",
                table: "Calories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Calories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Calories",
                newName: "IdCalories");
        }
    }
}
