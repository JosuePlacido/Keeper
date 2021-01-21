using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class StageMap : IEntityTypeConfiguration<Stage>
	{
		public void Configure(EntityTypeBuilder<Stage> builder)
		{
			builder.ToTable("tb_stage");

			builder.Property(stage => stage.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();

			builder.Property(stage => stage.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(stage => stage.Criterias)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.IsRequired();
			builder.HasMany(stage => stage.Groups)
				.WithOne()
				.HasForeignKey(group => group.StageId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
