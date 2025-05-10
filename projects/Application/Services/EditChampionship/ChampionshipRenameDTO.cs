namespace Application.Services.EditChampionship
{
	public class ObjectRenameDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public ObjectRenameDTO[] Childs { get; set; }
	}
}
