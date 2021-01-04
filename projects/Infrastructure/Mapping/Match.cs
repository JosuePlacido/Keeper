using System.Threading.Tasks.Dataflow;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class MatchMap : IEntityTypeConfiguration<Match>
	{
		public void Configure(EntityTypeBuilder<Match> builder)
		{
			builder.ToTable("tb_match");
			builder.Property(match => match.Id)
				.HasColumnName("Id").ValueGeneratedOnAdd();
			builder.Property(match => match.Name)
				.HasColumnType("varchar(50)")
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(match => match.Status)
				.HasColumnType("varchar(15)")
				.HasMaxLength(15)
				.HasDefaultValue(Status.Created)
				.IsRequired();

			builder.HasOne(match => match.VacancyAway);
			builder.HasOne(match => match.VacancyHome);
			builder.HasOne(match => match.Home);
			builder.HasOne(match => match.Away);
			builder.HasMany(match => match.EventGames)
				.WithOne(eventGame => eventGame.Match)
				.HasForeignKey(eventGame => eventGame.MatchId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
