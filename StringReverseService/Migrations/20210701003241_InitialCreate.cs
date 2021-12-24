using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StringReverseService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputStrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InputValue = table.Column<string>(nullable: false),
                    RequestedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputStrings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InputStrings",
                columns: new[] { "Id", "InputValue", "RequestedOn" },
                values: new object[] { new Guid("cfe5626b-d99d-4577-91a6-4171c0145035"), "TestValue", new DateTime(2021, 7, 1, 0, 32, 41, 437, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputStrings");
        }
    }
}
