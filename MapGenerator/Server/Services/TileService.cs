using Server.Models;
using Server.Response;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;
using Shared.Response.SuccessRespond;

namespace Server.Services;

public class TileService(DataContext dataContext)
{
   async Task<HandlerResult<Success, IErrorResult>> SaveTile()
   {
      return new EntityNotFound();
   }
}