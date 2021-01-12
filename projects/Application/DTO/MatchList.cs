using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
	public class MatchEditsScope
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public MatchGroupEdit[] Groups { get; set; }
	}
	public class MatchGroupEdit
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public MatchItemDTO[] Matchs { get; set; }
	}

	public class MatchItemDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime? Date { get; set; }
		public Vacancy VacancyHome { get; set; }
		public string VacancyHomeId { get; set; }
		public string VacancyAwayId { get; set; }
		public Vacancy VacancyAway { get; set; }
		public TeamSubscribe Home { get; set; }
		public string HomeId { get; set; }
		public TeamSubscribe Away { get; set; }
		public string AwayId { get; set; }
		public int Round { get; set; }
		public string Status { get; set; }
		public IDictionary<string, string> Errors { get; set; }

	}
}
