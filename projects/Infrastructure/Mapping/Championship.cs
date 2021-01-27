using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Keeper.Infrastructure.Mapping
{
	public class ChampionshipMap : IEntityTypeConfiguration<Championship>
	{
		public void Configure(EntityTypeBuilder<Championship> builder)
		{
			builder.ToTable("tb_championship");
			builder.Property(c => c.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();

			builder.Property(c => c.Name)
				.HasColumnType("varchar(100)")
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(c => c.Edition)
				.HasConversion(value => value, value => value)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(c => c.Status)
				.HasConversion(value => value, value => value)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.HasDefaultValue(Status.Created)
				.IsRequired();

			builder.HasOne(c => c.Category);

			builder.HasMany(c => c.Stages)
				.WithOne()
				.HasForeignKey(s => s.ChampionshipId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(c => c.Teams)
				.WithOne()
				.HasForeignKey(ts => ts.ChampionshipId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
