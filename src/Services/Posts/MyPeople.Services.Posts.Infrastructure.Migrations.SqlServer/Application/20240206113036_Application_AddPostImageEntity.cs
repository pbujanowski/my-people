#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.Application;

/// <inheritdoc />
public partial class Application_AddPostImageEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "PostImages",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>("uniqueidentifier", nullable: true),
                    ImageId = table.Column<Guid>("uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>("datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>("datetime2", nullable: true)
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