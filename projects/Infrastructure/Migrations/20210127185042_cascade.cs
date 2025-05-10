using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_team_subscribe_tb_championship_ChampionshipId",
                table: "tb_team_subscribe");

            migrationBuilder.AlterColumn<string>(
                name: "ChampionshipId",
                table: "tb_team_subscribe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_team_subscribe_tb_championship_ChampionshipId",
                table: "tb_team_subscribe",
                column: "ChampionshipId",
                principalTable: "tb_championship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_team_subscribe_tb_championship_ChampionshipId",
                table: "tb_team_subscribe");

            migrationBuilder.AlterColumn<string>(
                name: "ChampionshipId",
                table: "tb_team_subscribe",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_team_subscribe_tb_championship_ChampionshipId",
                table: "tb_team_subscribe",
                column: "ChampionshipId",
                principalTable: "tb_championship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
