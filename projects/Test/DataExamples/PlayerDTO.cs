using Keeper.Application.DTO;

namespace Test.DataExamples
{
	public static class PlayerDTODataExample
	{
		public static PlayerCreateDTO PlayerFull = new PlayerCreateDTO
		{
			Name = "Fulano",
			Nickname = "FULL"
		};
		public static PlayerCreateDTO PlayerInválid = new PlayerCreateDTO
		{
			Name = "",
		};
		public static PlayerUpdateDTO PlayerUpdateNameOnly = new PlayerUpdateDTO
		{
			Name = "Fulano Editado"
		};
		public static PlayerUpdateDTO PlayerUpdateNickname = new PlayerUpdateDTO
		{
			Id = "test",
			Name = "test",
			Nickname = "Alterado"
		};
	}
}
