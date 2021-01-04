using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidCategorySetup : TheoryData<Category>
	{
		public ValidCategorySetup()
		{
			foreach (var Category in CategoryDataExamples.GetValidBasic())
			{
				Add(Category);
			}
		}
	}
}
