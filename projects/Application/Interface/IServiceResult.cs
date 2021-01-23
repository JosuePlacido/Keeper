
using FluentValidation.Results;

namespace Keeper.Application.Interface
{
	public interface IServiceResult
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
