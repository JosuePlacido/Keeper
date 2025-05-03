using FluentValidation.Results;
using Application.Contract;

namespace Application.DTO
{
	public class ServiceResponse : IServiceResponse
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
