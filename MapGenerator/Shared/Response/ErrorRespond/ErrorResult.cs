using Shared.Response.IResponse;

namespace Shared.Response.ErrorRespond;

public class ErrorResult : IErrorResult
{
    public string Title { get; init; }
    public string Message { get; init; }
}