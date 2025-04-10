using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchBackend.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurServiceCategory");

            migrationBuilder.CreateTable(
                name: "OurServiceProject",
                columns: table => new
                {
                    OurServiceId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServiceProject", x => new { x.OurServiceId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_OurServiceProject_OurServices_OurServiceId",
                        column: x => x.OurServiceId,
                        principalTable: "OurServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OurServiceProject_Projects_OurServiceId",
                        column: x => x.OurServiceId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OurServiceProject");

            migrationBuilder.CreateTable(
                name: "OurServiceCategory",
                columns: table => new
                {
                    OurServiceId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServiceCategory", x => new { x.OurServiceId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_OurServiceCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OurServiceCategory_OurServices_OurServiceId",
                        column: x => x.OurServiceId,
                        principalTable: "OurServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OurServiceCategory_CategoryId",
                table: "OurServiceCategory",
                column: "CategoryId");
        }
    }
}
