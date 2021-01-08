using Application.DTO;
using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.Application.Mapping
{
	internal class CreateChampionshipSetup : TheoryData<ChampionshipCreateDTOModelTest>
	{
		public CreateChampionshipSetup()
		{
			foreach (var dto in ChampionshipCreateDTODataExamples.GetValid())
			{
				Add(dto);
			}
		}
	}
}
