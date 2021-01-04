using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class CategoryMap : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("tb_category");
			builder.Property(category => category.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property(category => category.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(category => category.Description)
				.HasConversion(value => value, value => value)
				.HasColumnType("varchar(200)")
				.HasMaxLength(200);
		}
	}
}
