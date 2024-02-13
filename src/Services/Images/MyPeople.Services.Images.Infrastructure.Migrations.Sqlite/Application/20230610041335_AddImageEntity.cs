#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Images.Infrastructure.Migrations.Sqlite.Application;

/// <inheritdoc />
public partial class AddImageEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Images",
            table =>
                new
                {
                    Id = table.Column<Guid>("TEXT", nullable: false),
                    Name = table.Column<string>("TEXT", nullable: false),
                    ContentType = table.Column<string>("TEXT", nullable: false),
                    Content = table.Column<string>("TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>("TEXT", nullable: false)
                },
            constraints: table => table.PrimaryKey("PK_Images", x => x.Id)
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Images");
    }
}