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
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.pl", true, "admin", true, "AQAAAAEAACcQAAAAEHKPH+B0FgGZue/91O5Xuy36W/RJWIre4Lk6Tw2XwNVOQKsR4DZgfpG6DTQztryPBw==", "14f6cb4b-294e-421e-8054-a8fbb34205dc", "admin", "admin@admin.pl" });

            migrationBuilder.InsertData(
                table: "UserPoints",
                columns: new[] { "Id", "CountOfPrize", "Points", "QrCodeToken" },
                values: new object[] { "14f6cb4b-294e-421e-8054-a8fbb34205dc", 0, 0, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPoints",
                keyColumn: "Id",
                keyValue: "14f6cb4b-294e-421e-8054-a8fbb34205dc");
        }
    }
}
