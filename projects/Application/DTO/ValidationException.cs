using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Application.DTO
{
	public class ValidationException : Exception
	{
		public IList<ValidationFailure> Errors { get; }

		public ValidationException(string message)
			: base(message)
		{
			Errors = new List<ValidationFailure>();
		}

		public ValidationException(string message, ValidationFailure[] errors)
			: base(message)
		{
			Errors = errors;
		}
	}
}