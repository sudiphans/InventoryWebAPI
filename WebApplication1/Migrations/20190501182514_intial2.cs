using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeService.Migrations
{
    public partial class intial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLoans_CDetailId",
                table: "CLoans");

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "CDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DvdType",
                table: "CDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "CDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CLoans_CDetailId",
                table: "CLoans",
                column: "CDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CLoans_CDetailId",
                table: "CLoans");

            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "CDetails");

            migrationBuilder.DropColumn(
                name: "DvdType",
                table: "CDetails");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "CDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CLoans_CDetailId",
                table: "CLoans",
                column: "CDetailId");
        }
    }
}
