using System;
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
		public virtual int FromStageOrder { set; get; } 
		public virtual int? FromPosition { get; set; }
		public virtual string GroupId { set; get; }
		public virtual Group Group { set; get; }
		public Vacancy() { }
		public Vacancy(string description, Classifieds ocupationType, string fromGroupId, int fromStageOrder, int? fromPosition)
		{
			Id = Guid.NewGuid().ToString();
			Description = description;
			OcupationType = ocupationType;
			FromGroupId = fromGroupId;
			FromStageOrder = fromStageOrder;
			FromPosition = fromPosition;
		}
		public override string ToString()
		{
			return Description;
		}
	}
}
