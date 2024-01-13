using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPeople.Services.Images.Infrastructure.Migrations.SqlServer.Application
{
    /// <inheritdoc />
    public partial class Application_AddImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Images");
        }
    }
}
