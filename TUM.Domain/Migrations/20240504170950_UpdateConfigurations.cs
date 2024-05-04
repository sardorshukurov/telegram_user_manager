using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TUM.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotUsers_Bots_BotId",
                table: "BotUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BotUsers_Users_UserId",
                table: "BotUsers");

            migrationBuilder.DropTable(
                name: "BotUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bots",
                table: "Bots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotUsers",
                table: "BotUsers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Bots",
                newName: "bots");

            migrationBuilder.RenameTable(
                name: "BotUsers",
                newName: "bots_users");

            migrationBuilder.RenameIndex(
                name: "IX_BotUsers_BotId",
                table: "bots_users",
                newName: "IX_bots_users_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bots",
                table: "bots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bots_users",
                table: "bots_users",
                columns: new[] { "UserId", "BotId" });

            migrationBuilder.CreateTable(
                name: "bots_admins",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    BotId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bots_admins", x => new { x.AdminId, x.BotId });
                    table.ForeignKey(
                        name: "FK_bots_admins_bots_BotId",
                        column: x => x.BotId,
                        principalTable: "bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bots_admins_users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bots_admins_BotId",
                table: "bots_admins",
                column: "BotId");

            migrationBuilder.AddForeignKey(
                name: "FK_bots_users_bots_BotId",
                table: "bots_users",
                column: "BotId",
                principalTable: "bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bots_users_users_UserId",
                table: "bots_users",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bots_users_bots_BotId",
                table: "bots_users");

            migrationBuilder.DropForeignKey(
                name: "FK_bots_users_users_UserId",
                table: "bots_users");

            migrationBuilder.DropTable(
                name: "bots_admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bots",
                table: "bots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bots_users",
                table: "bots_users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "bots",
                newName: "Bots");

            migrationBuilder.RenameTable(
                name: "bots_users",
                newName: "BotUsers");

            migrationBuilder.RenameIndex(
                name: "IX_bots_users_BotId",
                table: "BotUsers",
                newName: "IX_BotUsers_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bots",
                table: "Bots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotUsers",
                table: "BotUsers",
                columns: new[] { "UserId", "BotId" });

            migrationBuilder.CreateTable(
                name: "BotUser",
                columns: table => new
                {
                    AdminsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BotsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotUser", x => new { x.AdminsId, x.BotsId });
                    table.ForeignKey(
                        name: "FK_BotUser_Bots_BotsId",
                        column: x => x.BotsId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BotUser_Users_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotUser_BotsId",
                table: "BotUser",
                column: "BotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotUsers_Bots_BotId",
                table: "BotUsers",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotUsers_Users_UserId",
                table: "BotUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
