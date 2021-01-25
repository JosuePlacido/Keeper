using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Mapping
{
	public class TeamMap : IEntityTypeConfiguration<Team>
	{
		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.ToTable("tb_team");
			builder.Property(team => team.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property<string>("NormalizedName");
			builder.Property(team => team.Name)
				.HasColumnType("varchar(100)")
				.HasMaxLength(100)
				.IsRequired();
			builder.Property(team => team.Abrev)
				.HasColumnType("varchar(3)")
				.HasMaxLength(3);
			builder.Property(team => team.LogoUrl)
				.HasColumnType("varchar(200)")
				.HasMaxLength(200);
		}
	}
}
