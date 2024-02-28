using Shared.Response.IResponse;

namespace Shared.Response.SuccessRespond;

public class SuccessData<T> : ISuccessResult
{
    public T? Data { get; init; }
}