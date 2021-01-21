using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class EventGameMap : IEntityTypeConfiguration<EventGame>
	{
		public void Configure(EntityTypeBuilder<EventGame> builder)
		{
			builder.ToTable("tb_event_game");
			builder.Property(event_game => event_game.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();

			builder.Property(event_game => event_game.Description)
				.HasConversion(value => value, value => value)
				.HasColumnType("varchar(200)")
				.HasMaxLength(200)
				.IsRequired();

			builder.HasOne(event_game => event_game.RegisterPlayer);
		}
	}
}
