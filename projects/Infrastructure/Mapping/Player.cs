using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class PlayerMap : IEntityTypeConfiguration<Player>
	{
		public void Configure(EntityTypeBuilder<Player> builder)
		{
			builder.ToTable("tb_player");
			builder.Property(player => player.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property(player => player.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();
		}
	}
}
