using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.Models
{
	public class HookPlaces : Base
	{
		public virtual string Description { get; set; }
		public virtual Classifieds Regulation { get; set; }
		public virtual Group FromGroup { set; get; }
		public virtual string FromGroupId { set; get; }
		public virtual int FromPosition { get; set; }
		public virtual string OwnGroupId { set; get; }
		public virtual Group OwnGroup { set; get; }
		public virtual int Position { get; set; }
		public override string ToString()
		{
			return Description;
		}
	}
}
