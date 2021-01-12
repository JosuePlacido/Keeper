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
				.HasColumnName("Id")
				.IsRequired();
			builder.Property(teamSubscribe => teamSubscribe.Status)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.HasDefaultValue(Status.Created)
				.IsRequired();

			builder.HasMany(teamSubscribe => teamSubscribe.Players)
				.WithOne()
				.HasForeignKey(player => player.TeamSubscribeId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
