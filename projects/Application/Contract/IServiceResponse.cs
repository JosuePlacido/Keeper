
using FluentValidation.Results;

namespace Keeper.Application.Contract
{
	public interface IServiceResponse
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
