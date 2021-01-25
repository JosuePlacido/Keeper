using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class PlayerSubscribeMap : IEntityTypeConfiguration<PlayerSubscribe>
	{
		public void Configure(EntityTypeBuilder<PlayerSubscribe> builder)
		{
			builder.ToTable("tb_player_subscribe");
			builder.Property(player_subscribe => player_subscribe.Id)
				.HasColumnName("Id")
				.IsRequired();
			builder.HasOne(player_subscribe => player_subscribe.Player);
			builder.Property(player_subscribe => player_subscribe.Status)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.HasDefaultValue(Status.Matching)
				.IsRequired();
		}
	}
}
