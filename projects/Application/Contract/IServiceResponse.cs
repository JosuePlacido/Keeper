
using FluentValidation.Results;

namespace Application.Contract
{
	public interface IServiceResponse
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
