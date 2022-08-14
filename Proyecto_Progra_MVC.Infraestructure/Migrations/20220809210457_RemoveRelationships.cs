using Microsoft.EntityFrameworkCore.Migrations;

namespace Proyecto_Progra_MVC.Infraestructure.Migrations
{
    public partial class RemoveRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measures_AspNetUsers_IdUserId",
                table: "Measures");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_Measures_IdUserId",
                table: "Measures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "IdUserId",
                table: "Measures");

            migrationBuilder.CreateTable(
                name: "Calories",
                columns: table => new
                {
                    IdCalories = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasalMetabolism = table.Column<int>(type: "int", nullable: false),
                    MaintainCalories = table.Column<int>(type: "int", nullable: false),
                    LoseWeightCalories = table.Column<int>(type: "int", nullable: false),
                    GainWeightCalories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calories", x => x.IdCalories);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calories");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdUserId",
                table: "Measures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_IdUserId",
                table: "Measures",
                column: "IdUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_AspNetUsers_IdUserId",
                table: "Measures",
                column: "IdUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
