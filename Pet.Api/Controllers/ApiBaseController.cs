using Microsoft.AspNetCore.Mvc;
using System.Net;
using Pet.Core.Exceptions;

namespace Pet.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult ExceptionResult(Exception exception)
        {
            return exception switch
            {
                _ when exception is RepositoryException =>
                    StatusCode((int)HttpStatusCode.BadRequest, $"DB error: {exception.Message}" +
                                                                Environment.NewLine +
                                                                $"{exception?.InnerException?.Message}"),
                _ when exception is IApiException apiException =>
                    StatusCode((int)HttpStatusCode.BadRequest, apiException.Errors),

                _ =>
                    StatusCode((int)HttpStatusCode.BadRequest, $"Smtg went wrong: {exception.Message}" +
                                                                Environment.NewLine +
                                                                $"{exception?.InnerException?.Message}")
            };
        }
    }
}
