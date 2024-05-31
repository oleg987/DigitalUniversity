using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddauthInfosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    InviteCode = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthInfos_Email",
                table: "AuthInfos",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthInfos_InviteCode",
                table: "AuthInfos",
                column: "InviteCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthInfos_UserId",
                table: "AuthInfos",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthInfos");
        }
    }
}
