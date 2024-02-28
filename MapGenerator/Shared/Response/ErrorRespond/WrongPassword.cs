using Shared.Response.IResponse;

namespace Shared.Response.ErrorRespond;

public class WrongPassword : IErrorResult
{
    public string Title { get; init; } = "Wrong password";
    public string Message { get; init; } = "Niepoprawne Hasło";
}