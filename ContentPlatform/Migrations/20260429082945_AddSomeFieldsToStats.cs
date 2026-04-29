using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeFieldsToStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchevement_Achievement_AchievementId",
                table: "UserAchevement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchevement_Users_UserId",
                table: "UserAchevement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchevement",
                table: "UserAchevement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievement",
                table: "Achievement");

            migrationBuilder.RenameTable(
                name: "UserAchevement",
                newName: "UserAchevements");

            migrationBuilder.RenameTable(
                name: "Achievement",
                newName: "Achievements");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchevement_UserId",
                table: "UserAchevements",
                newName: "IX_UserAchevements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchevement_AchievementId",
                table: "UserAchevements",
                newName: "IX_UserAchevements_AchievementId");

            migrationBuilder.AddColumn<int>(
                name: "LowRatingsCount",
                table: "UserStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerfectiRatingCount",
                table: "UserStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchevements",
                table: "UserAchevements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchevements_Achievements_AchievementId",
                table: "UserAchevements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchevements_Users_UserId",
                table: "UserAchevements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchevements_Achievements_AchievementId",
                table: "UserAchevements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchevements_Users_UserId",
                table: "UserAchevements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchevements",
                table: "UserAchevements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "LowRatingsCount",
                table: "UserStats");

            migrationBuilder.DropColumn(
                name: "PerfectiRatingCount",
                table: "UserStats");

            migrationBuilder.RenameTable(
                name: "UserAchevements",
                newName: "UserAchevement");

            migrationBuilder.RenameTable(
                name: "Achievements",
                newName: "Achievement");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchevements_UserId",
                table: "UserAchevement",
                newName: "IX_UserAchevement_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchevements_AchievementId",
                table: "UserAchevement",
                newName: "IX_UserAchevement_AchievementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchevement",
                table: "UserAchevement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievement",
                table: "Achievement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchevement_Achievement_AchievementId",
                table: "UserAchevement",
                column: "AchievementId",
                principalTable: "Achievement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchevement_Users_UserId",
                table: "UserAchevement",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
