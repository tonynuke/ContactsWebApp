using Microsoft.AspNetCore.Mvc;

namespace Employee.WebService.Controllers
{
    public abstract class EnvelopController : ControllerBase
    {
        protected IActionResult Error(string errorMessage)
        {
            return base.BadRequest(Envelope.Error(errorMessage));
        }

        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }
    }
}