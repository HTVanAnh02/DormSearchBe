using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormSearchBe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    ApprovalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.ApprovalId);
                });

            migrationBuilder.CreateTable(
                name: "Areass",
                columns: table => new
                {
                    AreasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreasName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areass", x => x.AreasId);
                });

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HousesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFavorites = table.Column<bool>(type: "bit", nullable: false),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoritesId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Messages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessagesId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatus = table.Column<bool>(type: "bit", nullable: true),
                    IsFeedback = table.Column<bool>(type: "bit", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingsId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Roomstyles",
                columns: table => new
                {
                    RoomstyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomstyleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roomstyles", x => x.RoomstyleId);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HousesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HousesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acreage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressHouses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSubmitted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreasId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomstyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HousesId);
                    table.ForeignKey(
                        name: "FK_Houses_Areass_AreasId",
                        column: x => x.AreasId,
                        principalTable: "Areass",
                        principalColumn: "AreasId");
                    table.ForeignKey(
                        name: "FK_Houses_Citys_CityId",
                        column: x => x.CityId,
                        principalTable: "Citys",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Houses_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "FavoritesId");
                    table.ForeignKey(
                        name: "FK_Houses_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "RatingsId");
                    table.ForeignKey(
                        name: "FK_Houses_Roomstyles_RoomstyleId",
                        column: x => x.RoomstyleId,
                        principalTable: "Roomstyles",
                        principalColumn: "RoomstyleId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FavoritesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HousesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Active = table.Column<bool>(type: "bit", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "FavoritesId");
                    table.ForeignKey(
                        name: "FK_Users_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "HousesId");
                    table.ForeignKey(
                        name: "FK_Users_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessagesId");
                    table.ForeignKey(
                        name: "FK_Users_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "RatingsId");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Refresh_TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_AreasId",
                table: "Houses",
                column: "AreasId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_CityId",
                table: "Houses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_FavoritesId",
                table: "Houses",
                column: "FavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_RatingsId",
                table: "Houses",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_RoomstyleId",
                table: "Houses",
                column: "RoomstyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoritesId",
                table: "Users",
                column: "FavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HousesId",
                table: "Users",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MessageId",
                table: "Users",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RatingsId",
                table: "Users",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Areass");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Roomstyles");
        }
    }
}
