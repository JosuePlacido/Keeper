using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidEventGameSetup : TheoryData<EventGame>
	{
		public ValidEventGameSetup()
		{
			foreach (var event_game in EventGameDataExamples.GetValidBasic())
			{
				Add(event_game);
			}
		}
	}
}
