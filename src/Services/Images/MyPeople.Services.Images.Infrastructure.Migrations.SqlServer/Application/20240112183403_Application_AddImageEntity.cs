#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Images.Infrastructure.Migrations.SqlServer.Application;

/// <inheritdoc />
public partial class Application_AddImageEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Images",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                ContentType = table.Column<string>("nvarchar(max)", nullable: false),
                Content = table.Column<string>("nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>("datetime2", nullable: false)
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
        migrationBuilder.DropTable("Images");
    }
}
