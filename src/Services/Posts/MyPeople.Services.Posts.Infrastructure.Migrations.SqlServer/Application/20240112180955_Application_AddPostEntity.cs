#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.Application;

/// <inheritdoc />
public partial class Application_AddPostEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Posts",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Content = table.Column<string>("nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>("datetime2", nullable: false)
                },
            constraints: table => { table.PrimaryKey("PK_Posts", x => x.Id); }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Posts");
    }
}