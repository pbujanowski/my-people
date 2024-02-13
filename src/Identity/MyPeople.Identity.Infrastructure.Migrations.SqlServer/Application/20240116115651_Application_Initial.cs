#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPeople.Identity.Infrastructure.Migrations.SqlServer.Application;

/// <inheritdoc />
public partial class Application_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "AspNetRoles",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(
                        "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); }
        );

        migrationBuilder.CreateTable(
            "AspNetUsers",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(
                        "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedUserName = table.Column<string>(
                        "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(
                        "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    EmailConfirmed = table.Column<bool>("bit", nullable: false),
                    PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>("bit", nullable: false),
                    AccessFailedCount = table.Column<int>("int", nullable: false)
                },
            constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); }
        );

        migrationBuilder.CreateTable(
            "OpenIddictApplications",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ApplicationType = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    ClientId = table.Column<string>(
                        "nvarchar(100)",
                        maxLength: 100,
                        nullable: true
                    ),
                    ClientSecret = table.Column<string>("nvarchar(max)", nullable: true),
                    ClientType = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    ConcurrencyToken = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    ConsentType = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    DisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>("nvarchar(max)", nullable: true),
                    JsonWebKeySet = table.Column<string>("nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>("nvarchar(max)", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>("nvarchar(max)", nullable: true),
                    Properties = table.Column<string>("nvarchar(max)", nullable: true),
                    RedirectUris = table.Column<string>("nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>("nvarchar(max)", nullable: true),
                    Settings = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table => { table.PrimaryKey("PK_OpenIddictApplications", x => x.Id); }
        );

        migrationBuilder.CreateTable(
            "OpenIddictScopes",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ConcurrencyToken = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    Description = table.Column<string>("nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>("nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                    DisplayNames = table.Column<string>("nvarchar(max)", nullable: true),
                    Name = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>("nvarchar(max)", nullable: true),
                    Resources = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table => { table.PrimaryKey("PK_OpenIddictScopes", x => x.Id); }
        );

        migrationBuilder.CreateTable(
            "AspNetRoleClaims",
            table =>
                new
                {
                    Id = table
                        .Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            "AspNetUserClaims",
            table =>
                new
                {
                    Id = table
                        .Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetUserClaims_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            "AspNetUserLogins",
            table =>
                new
                {
                    LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>("nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey(
                    "PK_AspNetUserLogins",
                    x => new { x.LoginProvider, x.ProviderKey }
                );
                table.ForeignKey(
                    "FK_AspNetUserLogins_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            "AspNetUserRoles",
            table =>
                new
                {
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>("uniqueidentifier", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            "AspNetUserTokens",
            table =>
                new
                {
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                    Name = table.Column<string>("nvarchar(450)", nullable: false),
                    Value = table.Column<string>("nvarchar(max)", nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey(
                    "PK_AspNetUserTokens",
                    x =>
                        new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name
                        }
                );
                table.ForeignKey(
                    "FK_AspNetUserTokens_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            "OpenIddictAuthorizations",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>("uniqueidentifier", nullable: true),
                    ConcurrencyToken = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    CreationDate = table.Column<DateTime>("datetime2", nullable: true),
                    Properties = table.Column<string>("nvarchar(max)", nullable: true),
                    Scopes = table.Column<string>("nvarchar(max)", nullable: true),
                    Status = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>("nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                table.ForeignKey(
                    "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                    x => x.ApplicationId,
                    "OpenIddictApplications",
                    "Id"
                );
            }
        );

        migrationBuilder.CreateTable(
            "OpenIddictTokens",
            table =>
                new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>("uniqueidentifier", nullable: true),
                    AuthorizationId = table.Column<Guid>("uniqueidentifier", nullable: true),
                    ConcurrencyToken = table.Column<string>(
                        "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    CreationDate = table.Column<DateTime>("datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>("datetime2", nullable: true),
                    Payload = table.Column<string>("nvarchar(max)", nullable: true),
                    Properties = table.Column<string>("nvarchar(max)", nullable: true),
                    RedemptionDate = table.Column<DateTime>("datetime2", nullable: true),
                    ReferenceId = table.Column<string>(
                        "nvarchar(100)",
                        maxLength: 100,
                        nullable: true
                    ),
                    Status = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>("nvarchar(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                table.ForeignKey(
                    "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                    x => x.ApplicationId,
                    "OpenIddictApplications",
                    "Id"
                );
                table.ForeignKey(
                    "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                    x => x.AuthorizationId,
                    "OpenIddictAuthorizations",
                    "Id"
                );
            }
        );

        migrationBuilder.CreateIndex("IX_AspNetRoleClaims_RoleId", "AspNetRoleClaims", "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "AspNetRoles",
            "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL"
        );

        migrationBuilder.CreateIndex("IX_AspNetUserClaims_UserId", "AspNetUserClaims", "UserId");

        migrationBuilder.CreateIndex("IX_AspNetUserLogins_UserId", "AspNetUserLogins", "UserId");

        migrationBuilder.CreateIndex("IX_AspNetUserRoles_RoleId", "AspNetUserRoles", "RoleId");

        migrationBuilder.CreateIndex("EmailIndex", "AspNetUsers", "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "AspNetUsers",
            "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL"
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictApplications_ClientId",
            "OpenIddictApplications",
            "ClientId",
            unique: true,
            filter: "[ClientId] IS NOT NULL"
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
            "OpenIddictAuthorizations",
            new[] { "ApplicationId", "Status", "Subject", "Type" }
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictScopes_Name",
            "OpenIddictScopes",
            "Name",
            unique: true,
            filter: "[Name] IS NOT NULL"
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
            "OpenIddictTokens",
            new[] { "ApplicationId", "Status", "Subject", "Type" }
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_AuthorizationId",
            "OpenIddictTokens",
            "AuthorizationId"
        );

        migrationBuilder.CreateIndex(
            "IX_OpenIddictTokens_ReferenceId",
            "OpenIddictTokens",
            "ReferenceId",
            unique: true,
            filter: "[ReferenceId] IS NOT NULL"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("AspNetRoleClaims");

        migrationBuilder.DropTable("AspNetUserClaims");

        migrationBuilder.DropTable("AspNetUserLogins");

        migrationBuilder.DropTable("AspNetUserRoles");

        migrationBuilder.DropTable("AspNetUserTokens");

        migrationBuilder.DropTable("OpenIddictScopes");

        migrationBuilder.DropTable("OpenIddictTokens");

        migrationBuilder.DropTable("AspNetRoles");

        migrationBuilder.DropTable("AspNetUsers");

        migrationBuilder.DropTable("OpenIddictAuthorizations");

        migrationBuilder.DropTable("OpenIddictApplications");
    }
}