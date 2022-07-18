using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JPN.WebAPI.Core.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResult(object result = null)
        {
            if (ValidateOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Сообщения: ", Errors.ToArray()}
            }));
        }

        protected ActionResult CustomResult(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddErrors(error.ErrorMessage);   
            }

            return CustomResult();
        }

        protected ActionResult CustomResult(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AddErrors(erro.ErrorMessage);
            }

            return CustomResult();
        }

        protected bool ValidateOperation()
        {
            return !Errors.Any();
        }
        
        protected void AddErrors(string error)
        {
            Errors.Add(error);
        }

        protected void CleanErrors()
        {
            Errors.Clear();
        }
    }
}
