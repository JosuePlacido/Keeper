using Keeper.Domain.Core;

namespace Keeper.Application.DTO
{
	public class ObjectRenameDTO : IDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public ObjectRenameDTO[] Childs { get; set; }
	}
}
