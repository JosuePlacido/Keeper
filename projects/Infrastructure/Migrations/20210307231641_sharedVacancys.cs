using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class sharedVacancys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharedVacancyForNextStage",
                table: "tb_group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VacancyForNextStage",
                table: "tb_group",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedVacancyForNextStage",
                table: "tb_group");

            migrationBuilder.DropColumn(
                name: "VacancyForNextStage",
                table: "tb_group");
        }
    }
}
