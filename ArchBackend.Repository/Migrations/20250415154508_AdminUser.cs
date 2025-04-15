using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchBackend.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30f7972c-33e2-4cea-99b1-f47714e6b862", "AQAAAAIAAYagAAAAEEryn1LCktchcqJXTV/1ItRsYyWYQARN+pJaCf4QabD2FiV2BO1fhzynoEP+NqYttQ==", "FWP32LOOGHI37OPDB734EIYSTDARKAYD" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3a135f8-d8d9-4b7a-ba25-96b527f448d4", "AQAAAAIAAYagAAAAEHURDZI+vBC0F8b1taPkM9scfsE/XtTHAj/5e9yVymcpKNeVc271Fh1gFT0Qu/EeCA==", "JAVDBEVTJGWEHLJYS2WAINJUKEXZVS5K" });
        }
    }
}
