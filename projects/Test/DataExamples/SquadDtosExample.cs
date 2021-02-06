using Keeper.Application.Services.EditChampionship;
using Keeper.Domain.Enum;

namespace Test.DataExamples
{
	public class PlayerSquadPostDTOExample
	{
		public static PLayerSquadPostDTO[] Invalids = new PLayerSquadPostDTO[]{
				new PLayerSquadPostDTO(null, "p1", "false","player1", status: Status.Matching),
				new PLayerSquadPostDTO("noexist", "p6", "ts1","Dinho", status: Status.Matching),
				new PLayerSquadPostDTO("ps4", "noexist", "ts1","invalid", status: Status.Matching),
				new PLayerSquadPostDTO("ps2", "p11", "ts1","Delete", status: Status.FreeAgent),
			};
		public static PLayerSquadPostDTO[] Valids = new PLayerSquadPostDTO[]{
				new PLayerSquadPostDTO(null, "p11", "ts1","Delete", status: Status.Matching),
				new PLayerSquadPostDTO("ps1", "p5", "ts1","Palhinha", status: Status.FreeAgent),
				new PLayerSquadPostDTO("ps2", "p6", "ts2","Dinho", status: Status.Matching),
			};
	}
}
