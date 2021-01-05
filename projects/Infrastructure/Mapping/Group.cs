using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
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
				.WithOne(match => match.Group)
				.HasForeignKey(match => match.GroupId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(group => group.Statistics)
				.WithOne(statistics => statistics.Group)
				.HasForeignKey(statistics => statistics.GroupId);
			builder.HasMany(group => group.Vacancys)
				.WithOne(vacancy => vacancy.Group)
				.HasForeignKey(vacancy => vacancy.GroupId);

		}
	}
}
