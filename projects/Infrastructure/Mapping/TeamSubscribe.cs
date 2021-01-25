using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
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
				.HasDefaultValue(Status.Matching)
				.IsRequired();

			builder.HasMany(teamSubscribe => teamSubscribe.Players)
				.WithOne()
				.HasForeignKey(player => player.TeamSubscribeId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
