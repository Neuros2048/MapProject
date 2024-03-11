using Shared.Response.IResponse;

namespace Shared.Response.ErrorRespond;

public class UnauthorizedUser: IErrorResult
{
    public string Title { get; init; } = "Unauthorized User";
    public string Message { get; init; } = "Nie masz uprawnien do tego obiektu";
}