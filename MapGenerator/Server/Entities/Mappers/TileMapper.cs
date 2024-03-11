using Server.Entities.Entities;
using Server.Entities.Enumerations;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class TileMapper
{
    public static Tile DtoToTile(TileDto tileDto)
    {
        return new Tile
        {
            P0 = tileDto.P0,
            P1 = tileDto.P1,
            P2 = tileDto.P2,
            P3 = tileDto.P3,
            Stream = tileDto.Image,
            TileSetId = tileDto.TileSetId
        };
    }

    public static TileDto TileToDto(Tile tile)
    {
        return new TileDto
        {
            Image = tile.Stream,
            Id = tile.Id,
            P0 = tile.P0,
            P1 = tile.P1,
            P2 = tile.P2,
            P3 = tile.P3,
            TileSetId = tile.TileSetId
        };
    }
}