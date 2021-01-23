using FluentValidation.Results;
using Keeper.Application.Interface;

namespace Keeper.Application.Models
{
	public class ServiceResponse : IServiceResult
	{
		public ValidationResult ValidationResult { get; set; }
		public object Value { get; set; }
	}
}
