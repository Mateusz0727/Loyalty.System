using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loyalty.System.Data.Migrations
{
    public partial class add_qrCodeToken_column_to_UserPoints_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QrCodeToken",
                table: "UserPoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrCodeToken",
                table: "UserPoints");
        }
    }
}
