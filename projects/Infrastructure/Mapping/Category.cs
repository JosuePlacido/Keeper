using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class CategoryMap : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("tb_category");

			builder.Property<string>("Id").ValueGeneratedOnAdd();
			builder.HasKey("Id");
			builder.Property(category => category.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();
		}
	}
}
