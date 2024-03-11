using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class TileWeightMapper
{
    public static TileWeightDto TileWeightToDto(TileWeight tileWeight)
    {
        return new TileWeightDto
        {
            Weight = tileWeight.Weight,
            TileId = tileWeight.TileId,
            GeneratedMapId = tileWeight.GeneratedMapId
        };
    }

    public static TileWeightDto NewWeightDto(long tileId, long mapId)
    {
        return new TileWeightDto
        {
            Weight = 1,
            TileId = tileId,
            GeneratedMapId = mapId
        };
    }

    public static TileWeight DtoToTileWeight(TileWeightDto tileWeightDto)
    {
        return new TileWeight
        {
            Weight = tileWeightDto.Weight,
            GeneratedMapId = tileWeightDto.GeneratedMapId,
            TileId = tileWeightDto.TileId,
        };
    }
}