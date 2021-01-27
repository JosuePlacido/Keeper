﻿// <auto-generated />
using System;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210125210927_statusplayer")]
    partial class statusplayer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Keeper.Domain.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("tb_category");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Championship", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Edition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasDefaultValue("Criado");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("tb_championship");
                });

            modelBuilder.Entity("Keeper.Domain.Models.EventGame", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsHomeEvent")
                        .HasColumnType("bit");

                    b.Property<string>("MatchId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("RegisterPlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("RegisterPlayerId");

                    b.ToTable("tb_event_game");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("tb_group");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Match", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AggregateGame")
                        .HasColumnType("bit");

                    b.Property<int?>("AggregateGoalsAway")
                        .HasColumnType("int");

                    b.Property<int?>("AggregateGoalsHome")
                        .HasColumnType("int");

                    b.Property<string>("AwayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FinalGame")
                        .HasColumnType("bit");

                    b.Property<int?>("GoalsAway")
                        .HasColumnType("int");

                    b.Property<int?>("GoalsHome")
                        .HasColumnType("int");

                    b.Property<int?>("GoalsPenaltyAway")
                        .HasColumnType("int");

                    b.Property<int?>("GoalsPenaltyHome")
                        .HasColumnType("int");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Penalty")
                        .HasColumnType("bit");

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasDefaultValue("Criado");

                    b.Property<string>("VacancyAwayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VacancyHomeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AwayId");

                    b.HasIndex("GroupId");

                    b.HasIndex("HomeId");

                    b.HasIndex("VacancyAwayId");

                    b.HasIndex("VacancyHomeId");

                    b.ToTable("tb_match");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedNick")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tb_player");
                });

            modelBuilder.Entity("Keeper.Domain.Models.PlayerSubscribe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<int>("Games")
                        .HasColumnType("int");

                    b.Property<int>("Goals")
                        .HasColumnType("int");

                    b.Property<int>("MVPs")
                        .HasColumnType("int");

                    b.Property<string>("PlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RedCard")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasDefaultValue("Disputando");

                    b.Property<string>("TeamSubscribeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("YellowCard")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamSubscribeId");

                    b.ToTable("tb_player_subscribe");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Stage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("ChampionshipId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Criterias")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<bool>("DuplicateTurn")
                        .HasColumnType("bit");

                    b.Property<bool>("MirrorTurn")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Regulation")
                        .HasColumnType("int");

                    b.Property<int>("TypeStage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChampionshipId");

                    b.ToTable("tb_stage");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Statistic", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<int>("Drowns")
                        .HasColumnType("int");

                    b.Property<int>("Games")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsDifference")
                        .HasColumnType("int");

                    b.Property<int>("GoalsScores")
                        .HasColumnType("int");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Lastfive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lost")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("RankMovement")
                        .HasColumnType("int");

                    b.Property<int>("Reds")
                        .HasColumnType("int");

                    b.Property<string>("TeamSubscribeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Won")
                        .HasColumnType("int");

                    b.Property<int>("Yellows")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeamSubscribeId");

                    b.ToTable("tb_statistics");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Abrev")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tb_team");
                });

            modelBuilder.Entity("Keeper.Domain.Models.TeamSubscribe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("ChampionshipId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Drowns")
                        .HasColumnType("int");

                    b.Property<int>("Games")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsDifference")
                        .HasColumnType("int");

                    b.Property<int>("GoalsScores")
                        .HasColumnType("int");

                    b.Property<int>("Lost")
                        .HasColumnType("int");

                    b.Property<int>("Reds")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasDefaultValue("Disputando");

                    b.Property<string>("TeamId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Won")
                        .HasColumnType("int");

                    b.Property<int>("Yellows")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChampionshipId");

                    b.HasIndex("TeamId");

                    b.ToTable("tb_team_subscribe");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Vacancy", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FromGroupId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FromPosition")
                        .HasColumnType("int");

                    b.Property<int?>("FromStageOrder")
                        .HasColumnType("int");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OcupationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("tb_vacancy");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Championship", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Keeper.Domain.Models.EventGame", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Match", "Match")
                        .WithMany("EventGames")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Keeper.Domain.Models.PlayerSubscribe", "RegisterPlayer")
                        .WithMany()
                        .HasForeignKey("RegisterPlayerId");

                    b.Navigation("Match");

                    b.Navigation("RegisterPlayer");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Group", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Stage", null)
                        .WithMany("Groups")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Keeper.Domain.Models.Match", b =>
                {
                    b.HasOne("Keeper.Domain.Models.TeamSubscribe", "Away")
                        .WithMany()
                        .HasForeignKey("AwayId");

                    b.HasOne("Keeper.Domain.Models.Group", null)
                        .WithMany("Matchs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Keeper.Domain.Models.TeamSubscribe", "Home")
                        .WithMany()
                        .HasForeignKey("HomeId");

                    b.HasOne("Keeper.Domain.Models.Vacancy", "VacancyAway")
                        .WithMany()
                        .HasForeignKey("VacancyAwayId");

                    b.HasOne("Keeper.Domain.Models.Vacancy", "VacancyHome")
                        .WithMany()
                        .HasForeignKey("VacancyHomeId");

                    b.Navigation("Away");

                    b.Navigation("Home");

                    b.Navigation("VacancyAway");

                    b.Navigation("VacancyHome");
                });

            modelBuilder.Entity("Keeper.Domain.Models.PlayerSubscribe", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("Keeper.Domain.Models.TeamSubscribe", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamSubscribeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Stage", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Championship", null)
                        .WithMany("Stages")
                        .HasForeignKey("ChampionshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Keeper.Domain.Models.Statistic", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Group", null)
                        .WithMany("Statistics")
                        .HasForeignKey("GroupId");

                    b.HasOne("Keeper.Domain.Models.TeamSubscribe", "TeamSubscribe")
                        .WithMany()
                        .HasForeignKey("TeamSubscribeId");

                    b.Navigation("TeamSubscribe");
                });

            modelBuilder.Entity("Keeper.Domain.Models.TeamSubscribe", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Championship", null)
                        .WithMany("Teams")
                        .HasForeignKey("ChampionshipId");

                    b.HasOne("Keeper.Domain.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Vacancy", b =>
                {
                    b.HasOne("Keeper.Domain.Models.Group", null)
                        .WithMany("Vacancys")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Championship", b =>
                {
                    b.Navigation("Stages");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Group", b =>
                {
                    b.Navigation("Matchs");

                    b.Navigation("Statistics");

                    b.Navigation("Vacancys");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Match", b =>
                {
                    b.Navigation("EventGames");
                });

            modelBuilder.Entity("Keeper.Domain.Models.Stage", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Keeper.Domain.Models.TeamSubscribe", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
