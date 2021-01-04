using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidGroupSetup : TheoryData<Group>
	{
		public ValidGroupSetup()
		{
			foreach (var group in GroupDataExamples.GetValidBasic())
			{
				Add(group);
			}
		}
	}
}
