using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loyalty.System.Data.Migrations
{
    public partial class create_admin_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreatedUtc", "DateModifiedUtc", "Email", "EmailConfirmed", "GivenName", "IsAdmin", "Password", "PublicId", "Surname", "UserName" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.pl", true, "admin", true, "AQAAAAEAACcQAAAAEFwjyHqxyofwdpNmBq+7YUc3QNWs8YOnXKDdVkfgA6ZPBZvRx8cS9ZboEOa9ZcFOHw==", "f53b0bf3-1c74-43b7-9124-bd99749333e6", "admin", "admin@admin.pl" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
