using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class VacancyMap : IEntityTypeConfiguration<Vacancy>
	{
		public void Configure(EntityTypeBuilder<Vacancy> builder)
		{
			builder.ToTable("tb_vacancy");
			builder.Property(c => c.Id)
				.HasColumnName("Id")
				.IsRequired();
			builder.Property(c => c.Description)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();
		}
	}
}
