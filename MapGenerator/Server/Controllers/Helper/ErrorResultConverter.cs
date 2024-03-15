using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;

namespace Server.Controllers.Helper;

public static class ErrorResultConverter
{
    public static IActionResult ErrorResult(this ControllerBase controller, IErrorResult errorResult) => errorResult switch
    {
        EntityNotFound => controller.NotFound(errorResult),
        IncorrectData =>controller.BadRequest(errorResult),
        WrongPassword => controller.BadRequest(errorResult),
        UnauthorizedUser => controller.Unauthorized(errorResult),
        _ => controller.StatusCode((int)HttpStatusCode.BadRequest, errorResult)
    };
}