using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace EmployeeService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        ,
                    Lb_no = table.Column<int>(nullable: false),
                    sl_no = table.Column<string>(nullable: true),
                    make = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CLoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        ,
                    serNo = table.Column<int>(nullable: true),
                    CDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLoans_CDetails_CDetailId",
                        column: x => x.CDetailId,
                        principalTable: "CDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLoans_CDetailId",
                table: "CLoans",
                column: "CDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLoans");

            migrationBuilder.DropTable(
                name: "CDetails");
        }
    }
}
