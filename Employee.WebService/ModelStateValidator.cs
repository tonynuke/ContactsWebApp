using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Employee.WebService
{
    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
           var validationErrors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new ValidationResult(x.Key, x.Value.Errors.Select(y => y.ErrorMessage)));

            Envelope envelope = Envelope.Error(validationErrors);
            return new BadRequestObjectResult(envelope);
        }
    }
}