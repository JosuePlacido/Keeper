using Keeper.Infrastructure.CrossCutting.DTO;

namespace Test.DataExamples
{
	public static class TeamDTODataExample
	{
		public static TeamCreateDTO TeamFull = new TeamCreateDTO
		{
			Name = "São Paulo",
			Abrev = "SAO",
			LogoUrl = "https://i.pinimg.com/originals/e2/76/12/e27612d24ece4b8e5f2fc4f61e2e5938.jpg"
		};
		public static TeamUpdateDTO TeamUpdateNameOnly = new TeamUpdateDTO
		{
			Name = "São Paulo FC"
		};
	}
}
