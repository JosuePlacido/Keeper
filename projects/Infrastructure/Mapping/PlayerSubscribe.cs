using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
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
		}
	}
}
