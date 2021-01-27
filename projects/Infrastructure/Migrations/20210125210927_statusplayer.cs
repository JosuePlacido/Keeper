using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class statusplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "tb_team_subscribe",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Disputando",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "Criado");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tb_player_subscribe",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Disputando");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_player_subscribe");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "tb_team_subscribe",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Criado",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "Disputando");
        }
    }
}
