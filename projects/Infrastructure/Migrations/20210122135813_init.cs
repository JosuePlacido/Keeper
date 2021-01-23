using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_player",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_team",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Abrev = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    LogoUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_championship",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Edition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "Criado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_championship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_championship_tb_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_stage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DuplicateTurn = table.Column<bool>(type: "bit", nullable: false),
                    MirrorTurn = table.Column<bool>(type: "bit", nullable: false),
                    TypeStage = table.Column<int>(type: "int", nullable: false),
                    Criterias = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Regulation = table.Column<int>(type: "int", nullable: false),
                    ChampionshipId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_stage_tb_championship_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "tb_championship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_team_subscribe",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChampionshipId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "Criado"),
                    TeamId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Games = table.Column<int>(type: "int", nullable: false),
                    Won = table.Column<int>(type: "int", nullable: false),
                    Drowns = table.Column<int>(type: "int", nullable: false),
                    Lost = table.Column<int>(type: "int", nullable: false),
                    GoalsScores = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalsDifference = table.Column<int>(type: "int", nullable: false),
                    Yellows = table.Column<int>(type: "int", nullable: false),
                    Reds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_team_subscribe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_team_subscribe_tb_championship_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "tb_championship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_team_subscribe_tb_team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "tb_team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_group",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    StageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_group_tb_stage_StageId",
                        column: x => x.StageId,
                        principalTable: "tb_stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_player_subscribe",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamSubscribeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Games = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    YellowCard = table.Column<int>(type: "int", nullable: false),
                    RedCard = table.Column<int>(type: "int", nullable: false),
                    MVPs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_player_subscribe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_player_subscribe_tb_player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "tb_player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_player_subscribe_tb_team_subscribe_TeamSubscribeId",
                        column: x => x.TeamSubscribeId,
                        principalTable: "tb_team_subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_statistics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamSubscribeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Games = table.Column<int>(type: "int", nullable: false),
                    Won = table.Column<int>(type: "int", nullable: false),
                    Drowns = table.Column<int>(type: "int", nullable: false),
                    Lost = table.Column<int>(type: "int", nullable: false),
                    GoalsScores = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalsDifference = table.Column<int>(type: "int", nullable: false),
                    Yellows = table.Column<int>(type: "int", nullable: false),
                    Reds = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Lastfive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RankMovement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_statistics_tb_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "tb_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_statistics_tb_team_subscribe_TeamSubscribeId",
                        column: x => x.TeamSubscribeId,
                        principalTable: "tb_team_subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_vacancy",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OcupationType = table.Column<int>(type: "int", nullable: false),
                    FromGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromStageOrder = table.Column<int>(type: "int", nullable: true),
                    FromPosition = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_vacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_vacancy_tb_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "tb_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_match",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VacancyHomeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VacancyAwayId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HomeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AwayId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Round = table.Column<int>(type: "int", nullable: false),
                    GoalsHome = table.Column<int>(type: "int", nullable: true),
                    GoalsAway = table.Column<int>(type: "int", nullable: true),
                    GoalsPenaltyHome = table.Column<int>(type: "int", nullable: true),
                    GoalsPenaltyAway = table.Column<int>(type: "int", nullable: true),
                    FinalGame = table.Column<bool>(type: "bit", nullable: false),
                    AggregateGame = table.Column<bool>(type: "bit", nullable: false),
                    Penalty = table.Column<bool>(type: "bit", nullable: false),
                    AggregateGoalsAway = table.Column<int>(type: "int", nullable: true),
                    AggregateGoalsHome = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "Criado"),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_match_tb_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "tb_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_match_tb_team_subscribe_AwayId",
                        column: x => x.AwayId,
                        principalTable: "tb_team_subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_match_tb_team_subscribe_HomeId",
                        column: x => x.HomeId,
                        principalTable: "tb_team_subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_match_tb_vacancy_VacancyAwayId",
                        column: x => x.VacancyAwayId,
                        principalTable: "tb_vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_match_tb_vacancy_VacancyHomeId",
                        column: x => x.VacancyHomeId,
                        principalTable: "tb_vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_event_game",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsHomeEvent = table.Column<bool>(type: "bit", nullable: false),
                    MatchId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegisterPlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_event_game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_event_game_tb_match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "tb_match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_event_game_tb_player_subscribe_RegisterPlayerId",
                        column: x => x.RegisterPlayerId,
                        principalTable: "tb_player_subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_championship_CategoryId",
                table: "tb_championship",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_event_game_MatchId",
                table: "tb_event_game",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_event_game_RegisterPlayerId",
                table: "tb_event_game",
                column: "RegisterPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_group_StageId",
                table: "tb_group",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_match_AwayId",
                table: "tb_match",
                column: "AwayId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_match_GroupId",
                table: "tb_match",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_match_HomeId",
                table: "tb_match",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_match_VacancyAwayId",
                table: "tb_match",
                column: "VacancyAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_match_VacancyHomeId",
                table: "tb_match",
                column: "VacancyHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_player_subscribe_PlayerId",
                table: "tb_player_subscribe",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_player_subscribe_TeamSubscribeId",
                table: "tb_player_subscribe",
                column: "TeamSubscribeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stage_ChampionshipId",
                table: "tb_stage",
                column: "ChampionshipId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_statistics_GroupId",
                table: "tb_statistics",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_statistics_TeamSubscribeId",
                table: "tb_statistics",
                column: "TeamSubscribeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_team_subscribe_ChampionshipId",
                table: "tb_team_subscribe",
                column: "ChampionshipId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_team_subscribe_TeamId",
                table: "tb_team_subscribe",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_vacancy_GroupId",
                table: "tb_vacancy",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_event_game");

            migrationBuilder.DropTable(
                name: "tb_statistics");

            migrationBuilder.DropTable(
                name: "tb_match");

            migrationBuilder.DropTable(
                name: "tb_player_subscribe");

            migrationBuilder.DropTable(
                name: "tb_vacancy");

            migrationBuilder.DropTable(
                name: "tb_player");

            migrationBuilder.DropTable(
                name: "tb_team_subscribe");

            migrationBuilder.DropTable(
                name: "tb_group");

            migrationBuilder.DropTable(
                name: "tb_team");

            migrationBuilder.DropTable(
                name: "tb_stage");

            migrationBuilder.DropTable(
                name: "tb_championship");

            migrationBuilder.DropTable(
                name: "tb_category");
        }
    }
}
