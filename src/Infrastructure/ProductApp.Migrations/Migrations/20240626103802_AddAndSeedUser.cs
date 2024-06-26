using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddAndSeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ID", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "FirstName", "IsDeleted", "LastName", "Status" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 6, 26, 10, 38, 2, 11, DateTimeKind.Utc).AddTicks(6488), "John", false, "Doe", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
