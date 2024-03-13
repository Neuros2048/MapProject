using Microsoft.EntityFrameworkCore;
using Server.Entities.Entities;
using Server.Entities.Mappers;
using Server.Models;
using Server.Response;
using Shared.DTO;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;
using Shared.Response.SuccessRespond;

namespace Server.Services;

public class TileService(DataContext dataContext)
{
   public async Task<HandlerResult<Success, IErrorResult>> SaveTile()
   {
      return new EntityNotFound();
   }
   
   public async Task<HandlerResult<SuccessData<TileSetDto>, IErrorResult>> CreateTileSet(TileSetDto tileSetDto, long userId)
   {
      var result = await dataContext.TileSets.AddAsync(TileSetMapper.DtoToTileSet(tileSetDto,userId));
      await dataContext.SaveChangesAsync();
      return new SuccessData<TileSetDto>()
      {
         Data = TileSetMapper.TileSetToDto(result.Entity)
      };
   }
   public async Task<HandlerResult<SuccessData<List<TileSetDto>>, IErrorResult>> GetSets(long userId)
   {
      return new SuccessData<List<TileSetDto>>()
      {
         Data = await dataContext.TileSets.Where(x => x.UserId == userId).Select(x => TileSetMapper.TileSetToDto(x))
            .ToListAsync()
      };
   }
   
   public async Task<HandlerResult<Success, IErrorResult>> RemoveSet(long tileSetId)
   {
      var result = await dataContext.TileSets.FindAsync(tileSetId);
      if (result == null)
      {
         return new EntityNotFound();
      }

      dataContext.TileSets.Remove(result);
      await dataContext.SaveChangesAsync();
      return new Success();
   }
   
   public async Task<HandlerResult<SuccessData<TileDto>, IErrorResult>> AddTile(TileDto tileDto)
   {
      var result = await dataContext.Tiles.AddAsync(TileMapper.DtoToTile(tileDto));
      await dataContext.SaveChangesAsync();
      return new SuccessData<TileDto>()
      {
         Data = TileMapper.TileToDto(result.Entity)
      };
   }
   public async Task<HandlerResult<SuccessData<List<TileDto>>, IErrorResult>> GetTiles(long tileSetId)
   {
      return new SuccessData<List<TileDto>>()
      {
         Data = await dataContext.Tiles.Where(x => x.TileSetId == tileSetId).Select(x => TileMapper.TileToDto(x))
            .ToListAsync()
      };
   }
   
   public async Task<HandlerResult<Success, IErrorResult>> RemoveTile(long tileId)
   {
      var result = await dataContext.Tiles.FindAsync(tileId);
      if (result == null) return new EntityNotFound();
      var tileWeighs = await dataContext.Weights.Where(x => x.TileId == tileId).ToListAsync();
      var setTiles = await dataContext.SetTiles.Where(x => x.TileId == tileId).ToListAsync();
      foreach (var t in tileWeighs)
      {
         dataContext.Weights.Remove(t);
      }
      foreach (var t in setTiles)
      {
         dataContext.SetTiles.Remove(t);
      }
      dataContext.Tiles.Remove(result);
      await dataContext.SaveChangesAsync();
      return new Success();
   }
   public async Task<HandlerResult<Success, IErrorResult>> UpdateTile(TileDto tileDto)
   {
      var result = await dataContext.Tiles.FindAsync(tileDto.Id);
      if (result == null) return new EntityNotFound();
      result.P0 = tileDto.P0;
      result.P1 = tileDto.P1;
      result.P2 = tileDto.P2;
      result.P3 = tileDto.P3;
      return new Success();
   }

   public async Task<HandlerResult<Success, IErrorResult>> CheckRightsSet(long tileSetId, long userId)
   {
      var result = await dataContext.TileSets.FindAsync(tileSetId);
      if(result == null) return new UnauthorizedUser();
      if(result.UserId != userId) return new UnauthorizedUser();
      return new Success();
   }
   
   public async Task<HandlerResult<Success, IErrorResult>> CheckRightsTile(long tileId, long userId)
   {
      var result = await dataContext.Tiles.FindAsync(tileId);
      if(result == null) return new UnauthorizedUser();
      return await CheckRightsSet(result.TileSetId, userId);
   }
   
   public async Task<HandlerResult<Success, IErrorResult>> CheckRightsMap(long mapId, long userId)
   {
      var result = await dataContext.GeneratedMaps.FindAsync(mapId);
      if(result == null) return new UnauthorizedUser();
      return await CheckRightsSet(result.TileSetId, userId);
   }

   public async Task<HandlerResult<SuccessData<GeneratedMapDto>, IErrorResult>> PrepareNewMap(long tileSetId)
   {
      GeneratedMapDto mapDto = new GeneratedMapDto
      {
         TileWeightDtos = await dataContext.Tiles.Where(x => x.TileSetId == tileSetId)
            .Select(x => TileWeightMapper.NewWeightDto(x.Id, 0)).ToListAsync(),
         SetTileDtos = new List<SetTileDto>()
      };
      return new SuccessData<GeneratedMapDto>()
      {
         Data = mapDto
      };
   }

   public async Task<HandlerResult<Success, IErrorResult>> SaveGeneratedMap(GeneratedMapDto mapDto)
   {
      GeneratedMap map = GeneratedMapMapper.DtoToMap(mapDto);
      var mapRes = await dataContext.GeneratedMaps.AddAsync(map);
      foreach (var t in mapDto.TileWeightDtos)
      {
         TileWeight tileWeight = TileWeightMapper.DtoToTileWeight(t);
         tileWeight.GeneratedMap = mapRes.Entity;
         await dataContext.Weights.AddAsync(tileWeight);
      }
      foreach (var t in mapDto.SetTileDtos)
      {
         SetTile setTile = SetTileMapper.DtoToSetTile(t);
         setTile.GeneratedMap = mapRes.Entity;
         await dataContext.SetTiles.AddAsync(setTile);
      }
      await dataContext.SaveChangesAsync();
      return new Success();
   }

   public async Task<HandlerResult<SuccessData<List<GeneratedMapDto>>, IErrorResult>> GetMaps(long tileSetId)
   {
      return new SuccessData<List<GeneratedMapDto>>()
      {
         Data = await dataContext.GeneratedMaps.Where(x => x.TileSetId == tileSetId)
         .Select(x => GeneratedMapMapper.MapToDto(x)).ToListAsync()
      };
   }

   public async Task<HandlerResult<SuccessData<GeneratedMapDto>, IErrorResult>> GetMap(long mapId)
   {
      var resMap = await dataContext.GeneratedMaps.FindAsync(mapId);
      if (resMap == null) return new EntityNotFound();
      var weightList = await dataContext.Weights.Where(x => x.GeneratedMapId == mapId)
         .Select(x => TileWeightMapper.TileWeightToDto(x)).ToListAsync();
      var setTileList = await dataContext.SetTiles.Where(x => x.GeneratedMapId == mapId)
         .Select(x => SetTileMapper.SetTileToDto(x)).ToListAsync();
      GeneratedMapDto generatedMapDto = GeneratedMapMapper.MapToDto(resMap);
      generatedMapDto.SetTileDtos = setTileList;
      generatedMapDto.TileWeightDtos = weightList;
      return new SuccessData<GeneratedMapDto>()
      {
         Data = generatedMapDto
      };
   }
   
   public async Task<HandlerResult<SuccessData<TileDto>, IErrorResult>> GetBaseTile()
   {
      Tile tile = await dataContext.Tiles.FindAsync(1);
      return new SuccessData<TileDto>()
      {
         Data = TileMapper.TileToDto(tile)
      };
   }
   
}