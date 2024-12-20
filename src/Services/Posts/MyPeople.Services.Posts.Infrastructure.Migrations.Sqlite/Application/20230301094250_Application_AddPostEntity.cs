#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite.Application;

/// <inheritdoc />
public partial class Application_AddPostEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Posts",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                UserId = table.Column<Guid>("TEXT", nullable: false),
                Content = table.Column<string>("TEXT", nullable: false),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: false),
            },
            constraints: table => table.PrimaryKey("PK_Posts", x => x.Id)
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Posts");
    }
}
