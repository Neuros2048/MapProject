using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class SetTileMapper
{
    public static SetTileDto SetTileToDto(SetTile setTile)
    {
        return new SetTileDto
        {
            N = setTile.N,
            M = setTile.M,
            TileId = setTile.TileId
        };
    }

    public static SetTile DtoToSetTile(SetTileDto setTileDto)
    {
        return new SetTile
        {
            N = setTileDto.N,
            M = setTileDto.M,
            TileId = setTileDto.TileId,
        };
    }
}