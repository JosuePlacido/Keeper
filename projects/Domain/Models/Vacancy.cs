using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keeper.Domain.Core;
using Keeper.Domain.Enum;

namespace Keeper.Domain.Models
{
	public class Vacancy : Entity
	{
		public string Description { get; private set; }
		public Classifieds OcupationType { get; private set; }
		public string FromGroupId { get; private set; }
		public int? FromStageOrder { get; private set; }
		public int? FromPosition { get; private set; }
		public string GroupId { get; private set; }
		private Vacancy() { }

		public Vacancy(string description, Classifieds ocupationType,
			int? fromStageOrder = null, int? fromPosition = null) : base(Guid.NewGuid().ToString())
		{
			Description = description;
			OcupationType = ocupationType;
			FromStageOrder = fromStageOrder;
			FromPosition = fromPosition;
		}

		public override string ToString()
		{
			return Description;
		}

		public void AddReferenceFromGroup(string fromGroup)
		{
			FromGroupId = fromGroup;
		}
		public static Vacancy Factory(string id, string description, string groupId,
		 Classifieds ocupationType = Classifieds.Random, string fromGroupId = null,
		 int? fromStageOrder = null, int? fromPosition = null)
		{
			return new Vacancy
			{
				Id = id,
				Description = description,
				OcupationType = ocupationType,
				FromGroupId = fromGroupId,
				FromStageOrder = fromStageOrder,
				FromPosition = fromPosition,
				GroupId = groupId,
			};
		}
	}
}
