using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPeople.Services.Posts.Infrastructure.Migrations.SqlServer.Application
{
    /// <inheritdoc />
    public partial class Application_AddPostImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostImages",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostImages_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_PostId",
                table: "PostImages",
                column: "PostId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PostImages");
        }
    }
}
