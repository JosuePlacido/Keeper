using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class StatisticsMap : IEntityTypeConfiguration<Statistic>
	{
		public void Configure(EntityTypeBuilder<Statistic> builder)
		{
			builder.ToTable("tb_statistics");
			builder.Property(statistics => statistics.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property(statistics => statistics.Position).HasDefaultValue(1);

			builder.HasOne(statistics => statistics.TeamSubscribe);
		}
	}
}
