using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enum;

namespace Infrastructure.Mapping
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
				.HasColumnType("varchar(100)")
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(c => c.Status)
				.HasConversion(value => value, value => value)
				.HasColumnType("varchar(15)")
				.HasMaxLength(100)
				.HasDefaultValue(Status.Created)
				.IsRequired();

			builder.HasOne(c => c.Category);

			builder.HasMany(c => c.Stages)
				.WithOne(s => s.Championship)
				.HasForeignKey(s => s.ChampionshipId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
