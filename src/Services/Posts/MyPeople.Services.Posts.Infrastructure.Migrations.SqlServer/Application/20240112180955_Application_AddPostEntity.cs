using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.Application
{
    /// <inheritdoc />
    public partial class Application_AddPostEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Posts");
        }
    }
}
