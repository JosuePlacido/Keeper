using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class playersubscribefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_player_subscribe");

            migrationBuilder.AddColumn<string>(
                name: "ChampionshipId",
                table: "tb_player_subscribe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeAgent",
                table: "tb_player_subscribe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tb_player",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_player_subscribe_ChampionshipId",
                table: "tb_player_subscribe",
                column: "ChampionshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_player_subscribe_tb_championship_ChampionshipId",
                table: "tb_player_subscribe",
                column: "ChampionshipId",
                principalTable: "tb_championship",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_player_subscribe_tb_championship_ChampionshipId",
                table: "tb_player_subscribe");

            migrationBuilder.DropIndex(
                name: "IX_tb_player_subscribe_ChampionshipId",
                table: "tb_player_subscribe");

            migrationBuilder.DropColumn(
                name: "ChampionshipId",
                table: "tb_player_subscribe");

            migrationBuilder.DropColumn(
                name: "IsFreeAgent",
                table: "tb_player_subscribe");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tb_player");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tb_player_subscribe",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Disputando");
        }
    }
}
