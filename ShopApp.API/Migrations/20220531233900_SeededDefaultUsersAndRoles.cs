using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApp.API.Migrations
{
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b636a22-d6ae-4894-9af6-1851b754427c", "216201c7-90f3-42ad-b544-5bc7383378ee", "User", "USER" },
                    { "af68e839-3c0f-4840-9f9f-c874990c36c0", "6ec2cad4-4f77-4604-acf7-58790b0a55c7", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8541c232-f94f-4638-b500-1f6ed7eb2c0f", 0, "b78d1000-9cbf-478b-a832-6c272ca6209a", "tenas@gmail.com", false, "Tenas", "Steve", false, null, "TENAS@GMAIL.COM", "STEVETENAS", "AQAAAAEAACcQAAAAEGY+fxaY+Ty9GUueZdMh5ABngi17j0jspAWv8kCwU/Qr/74E3dQ1ro7TY1k0W0F9mQ==", null, false, "7a5ccb1f-8fcb-4b2a-9414-3ce8b6167e82", false, "stevetenas" },
                    { "8873b9a5-8f65-4718-8fff-4ee2f6593ac8", 0, "3e8f523a-3136-49f9-97f0-33b5582cb88d", "user@gmail.com", false, "System", "User", false, null, "USER@GMAIL.COM", "SYSTEMUSER", "AQAAAAEAACcQAAAAEADhvfS1l6tUAR0moTBnPPXr9fF2Y6kU5Mrb1Yn3Pyh8v7w6xvoaHySY4HY1q+Yz7w==", null, false, "c3a5dcff-b4be-42ad-a663-27c44a7de65d", false, "systemuser" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7b636a22-d6ae-4894-9af6-1851b754427c", "8541c232-f94f-4638-b500-1f6ed7eb2c0f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "af68e839-3c0f-4840-9f9f-c874990c36c0", "8873b9a5-8f65-4718-8fff-4ee2f6593ac8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7b636a22-d6ae-4894-9af6-1851b754427c", "8541c232-f94f-4638-b500-1f6ed7eb2c0f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af68e839-3c0f-4840-9f9f-c874990c36c0", "8873b9a5-8f65-4718-8fff-4ee2f6593ac8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b636a22-d6ae-4894-9af6-1851b754427c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af68e839-3c0f-4840-9f9f-c874990c36c0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8541c232-f94f-4638-b500-1f6ed7eb2c0f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8873b9a5-8f65-4718-8fff-4ee2f6593ac8");
        }
    }
}
