using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchBackend.Repository.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OurServices");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "OurServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OurServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "OurServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
