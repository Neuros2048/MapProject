using Shared.Response.IResponse;

namespace Shared.Response.SuccessRespond;

public class SuccessResult : ISuccessResult , INotificationResult
{
    public string Title { get; init; }
    public string Message { get; init; }
}