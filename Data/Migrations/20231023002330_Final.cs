using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchDayAnalyzerFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTeam_Games_GameId",
                table: "GameTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTeam_Teams_TeamId",
                table: "GameTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTeam",
                table: "GameTeam");

            migrationBuilder.DropIndex(
                name: "IX_GameTeam_GamesId",
                table: "GameTeam");

            migrationBuilder.DropIndex(
                name: "IX_GameTeam_TeamId",
                table: "GameTeam");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameTeam");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "GameTeam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTeam",
                table: "GameTeam",
                columns: new[] { "GamesId", "TeamsPlayedGameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTeam",
                table: "GameTeam");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameTeam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "GameTeam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTeam",
                table: "GameTeam",
                columns: new[] { "GameId", "TeamId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GameTeam_GamesId",
                table: "GameTeam",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeam_TeamId",
                table: "GameTeam",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTeam_Games_GameId",
                table: "GameTeam",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTeam_Teams_TeamId",
                table: "GameTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
