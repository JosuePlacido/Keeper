using System.Threading.Tasks.Dataflow;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class TeamSubscribeMap : IEntityTypeConfiguration<TeamSubscribe>
	{
		public void Configure(EntityTypeBuilder<TeamSubscribe> builder)
		{
			builder.ToTable("tb_team_subscribe");
			builder.Property(teamSubscribe => teamSubscribe.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property(teamSubscribe => teamSubscribe.Status)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.HasDefaultValue(Status.Created)
				.IsRequired();

			builder.HasOne(teamSubscribe => teamSubscribe.Championship);
			builder.HasOne(teamSubscribe => teamSubscribe.Team);

			builder.HasMany(teamSubscribe => teamSubscribe.Players)
				.WithOne(player => player.TeamSubscribe)
				.HasForeignKey(player => player.TeamSubscribeId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(teamSubscribe => teamSubscribe.Statistics)
				.WithOne(statistics => statistics.TeamSubscribe)
				.HasForeignKey(statistics => statistics.TeamSubscribeId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
