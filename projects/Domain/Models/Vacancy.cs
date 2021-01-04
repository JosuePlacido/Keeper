using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.Models
{
	public class Vacancy : Base
	{
		public virtual string Description { get; set; }
		public virtual Classifieds OcupationType { get; set; }
		public virtual string FromGroupId { set; get; }
		public virtual int? FromPosition { get; set; }
		public virtual string GroupId { set; get; }
		public virtual Group Group { set; get; }
		public override string ToString()
		{
			return Description;
		}
	}
}
