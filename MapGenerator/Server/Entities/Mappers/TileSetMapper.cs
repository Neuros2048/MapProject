using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class TileSetMapper
{
    public static TileSetDto TileSetToDto(TileSet tileSet)
    {
        return new TileSetDto()
        {
            Name = tileSet.Name,
            Id = tileSet.Id
        };
    }

    public static TileSet DtoToTileSet(TileSetDto tileSetDto, long userId)
    {
        return new TileSet()
        {
            Name = tileSetDto.Name,
            UserId = userId
        };
    }

    public static TileSet TileSetToNewTileSet(TileSet tileSet, long userId)
    {
        var set = new TileSet();
        set.Name = tileSet.Name;
        set.UserId = userId;
        set.Tiles = new List<Tile>();
        foreach (var t in tileSet.Tiles)
        {
            set.Tiles.Add( TileMapper.TileToNewTile(t));
        }


        return set;
    }
}