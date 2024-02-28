using Shared.Response.IResponse;

namespace Shared.Response.ErrorRespond;

public class IncorrectData : IErrorResult
{
    public string Title { get; init; } = "Incorrect Data";
    public string Message { get; init; } = "Dane są niepoprawne";
}