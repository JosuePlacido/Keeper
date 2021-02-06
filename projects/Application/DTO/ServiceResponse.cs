using FluentValidation.Results;
using Keeper.Application.Contract;

namespace Keeper.Application.DTO
{
	public class ServiceResponse : IServiceResponse
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
