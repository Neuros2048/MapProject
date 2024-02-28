using Shared.Response.IResponse;

namespace Shared.Response.ErrorRespond;

public class EntityNotFound : IErrorResult
{
    public string Title { get; init; } = "Entity not found";
    public string Message { get; init; } = "Nie znaleziono obiektu";
}