using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidStageSetup : TheoryData<Stage>
	{
		public ValidStageSetup()
		{
			foreach (var stage in StageDataExamples.GetValidBasic())
			{
				Add(stage);
			}
		}
	}
}
