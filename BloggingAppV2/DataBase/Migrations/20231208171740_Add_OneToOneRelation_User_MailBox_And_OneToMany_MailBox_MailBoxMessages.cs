using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloggingAppV2.Migrations
{
    /// <inheritdoc />
    public partial class Add_OneToOneRelation_User_MailBox_And_OneToMany_MailBox_MailBoxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailBoxes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailBoxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SentAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    MailBoxId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailBoxMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailBoxMessages_MailBoxes_MailBoxId",
                        column: x => x.MailBoxId,
                        principalTable: "MailBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequestsMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FromUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ToUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Accepted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequestsMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequestsMessages_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequestsMessages_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequestsMessages_MailBoxMessages_Id",
                        column: x => x.Id,
                        principalTable: "MailBoxMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdminName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMessages_MailBoxMessages_Id",
                        column: x => x.Id,
                        principalTable: "MailBoxMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequestsMessages_FromUserId",
                table: "FriendRequestsMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequestsMessages_ToUserId",
                table: "FriendRequestsMessages",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MailBoxes_UserId",
                table: "MailBoxes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailBoxMessages_MailBoxId",
                table: "MailBoxMessages",
                column: "MailBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequestsMessages");

            migrationBuilder.DropTable(
                name: "SystemMessages");

            migrationBuilder.DropTable(
                name: "MailBoxMessages");

            migrationBuilder.DropTable(
                name: "MailBoxes");
        }
    }
}
