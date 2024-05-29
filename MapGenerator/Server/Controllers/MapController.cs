using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Helper;
using Server.Services;
using Shared.DTO;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MapController(TileService tileService) : ControllerBase
{
    
   
    
    [HttpGet("get2")]
    public async Task<IActionResult> get2()
    {
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine(userId);
        if (userId == null) return Ok("dasdas");
        return Ok(userId);
        
   
    }
    
    [HttpGet("getTileSets"),Authorize]
    public async Task<IActionResult> TileSets()
    {
         var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
         var userId = idString == null ? 0 : long.Parse(idString);
        return (await tileService.GetSets(userId)).Match(Ok, this.ErrorResult);
    }
    [HttpPut("addTileToSet"),Authorize]
    public async Task<IActionResult> AddTile(TileDto tileDto)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        return (await tileService.AddTile(tileDto)).Match(Ok, this.ErrorResult); 
    }
    
    [HttpGet("GetTiles"),Authorize]
    public async Task<IActionResult> GetTiles(long tileSetId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsSet(tileSetId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.GetTiles(tileSetId)).Match(Ok, this.ErrorResult); 
    }
    
    [HttpPut("CreatTileSet"),Authorize]
    public async Task<IActionResult> CreateTileSet(TileSetDto tileSetDto)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        return (await tileService.CreateTileSet(tileSetDto,userId)).Match(Ok, this.ErrorResult); 
    }
    [HttpDelete("DeleteTileSet"),Authorize]
    public async Task<IActionResult> DeleteTileSet(long tileSetId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsSet(tileSetId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.RemoveSet(tileSetId)).Match(Ok, this.ErrorResult); 
    }
    [HttpDelete("DeleteTile"),Authorize]
    public async Task<IActionResult> DeleteTile(long tileId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsTile(tileId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.RemoveTile(tileId)).Match(Ok, this.ErrorResult); 
    }
    [HttpPost("UpdateTile"),Authorize]
    public async Task<IActionResult> UpdateTile(TileDto tileDto)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsTile(tileDto.Id, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.UpdateTile(tileDto)).Match(Ok, this.ErrorResult); 
    }
    
    [HttpGet("PrepareNewMap"),Authorize]
    public async Task<IActionResult> PrepareNewMap(long tileSetId)
    {
       var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
       var userId = idString == null ? 0 : long.Parse(idString);
       var res = await tileService.CheckRightsSet(tileSetId, userId);
       if (res.IsError) return res.Match(Ok, this.ErrorResult);
       return (await tileService.PrepareNewMap(tileSetId)).Match(Ok, this.ErrorResult); 
    }
    
    [HttpPut("SaveGeneratedMap"),Authorize]
    public async Task<IActionResult> SaveGeneratedMap(GeneratedMapDto mapDto)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsSet(mapDto.TileSetId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.SaveGeneratedMap(mapDto)).Match(Ok, this.ErrorResult); 
    }
    
    
    [HttpGet("GetMaps"),Authorize]
    public async Task<IActionResult> GetMaps(long tileSetId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsSet(tileSetId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.GetMaps(tileSetId)).Match(Ok, this.ErrorResult); 
    }

    [HttpGet("GetMap"),Authorize]
    public async Task<IActionResult> GetMap(long mapId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        var res = await tileService.CheckRightsMap(mapId, userId);
        if (res.IsError) return res.Match(Ok, this.ErrorResult);
        return (await tileService.GetMap(mapId)).Match(Ok, this.ErrorResult); 
    }
   
    [HttpGet("GetBaseTile")]
    public async Task<IActionResult> GetBaseTile()
    {
        return (await tileService.GetBaseTile()).Match(Ok, this.ErrorResult); 
    }
    
    [HttpGet("CopyTileSet"),Authorize]
    public async Task<IActionResult> CopyTileSet(long tileId)
    {
        var idString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = idString == null ? 0 : long.Parse(idString);
        return (await tileService.CopyTileSet(tileId,userId) ).Match(Ok, this.ErrorResult);
    }
  
    
}