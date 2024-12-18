#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Posts.Infrastructure.Migrations.Sqlite.Application;

/// <inheritdoc />
public partial class Application_AddPostImageEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "PostImages",
            table => new
            {
                Id = table.Column<Guid>("TEXT", nullable: false),
                PostId = table.Column<Guid>("TEXT", nullable: true),
                ImageId = table.Column<Guid>("TEXT", nullable: true),
                CreatedAt = table.Column<DateTime>("TEXT", nullable: true),
                UpdatedAt = table.Column<DateTime>("TEXT", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PostImages", x => x.Id);
                table.ForeignKey("FK_PostImages_Posts_PostId", x => x.PostId, "Posts", "Id");
            }
        );

        migrationBuilder.CreateIndex("IX_PostImages_PostId", "PostImages", "PostId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("PostImages");
    }
}
