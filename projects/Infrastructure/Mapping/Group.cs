using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class GroupMap : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.ToTable("tb_group");
			builder.Property(group => group.Id)
				.HasColumnName("Id")
				.IsRequired();
			builder.Property(group => group.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();

			builder.HasMany(group => group.Matchs)
				.WithOne()
				.HasForeignKey(match => match.GroupId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(group => group.Statistics)
				.WithOne()
				.HasForeignKey(statistics => statistics.GroupId);
			builder.HasMany(group => group.Vacancys)
				.WithOne()
				.HasForeignKey(vacancy => vacancy.GroupId);

		}
	}
}
