using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DTO;
using System;

namespace Api.Controllers;
[ApiController]
public abstract class ApiController : ControllerBase
{
	protected async Task<ActionResult> CallApplicationAsync<T>(Task<T> applicationCall)
	{
		try
		{
			var result = await applicationCall;
			return Ok(result);
		}
		catch (ValidationException ex)
		{
			foreach (var error in ex.Errors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
			return ValidationProblem(ModelState);
		}
		catch (Exception ex)
		{
			//TODO add surprise error management
			return StatusCode(500, "Ocorreu um erro inesperado.");
		}
	}
}
