using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class PolygonMapMapper
{
    public static PolygonMapDto PolygonMapToDto(PolygonMap polygonMap)
    {
        return new PolygonMapDto
        {
            Id = polygonMap.Id,
            Seed = polygonMap.Seed,
            Height = polygonMap.Height,
            Width = polygonMap.Width,
            Polygons = polygonMap.Polygons,
            Name = polygonMap.Name,
            HumidityPoints = polygonMap.HumidityPoints != null ? polygonMap.HumidityPoints.Select(MapPointsMapper.MapPointToDto).ToList() : [],
            HeightPoints = polygonMap.HeightPoints != null ? polygonMap.HeightPoints.Select(MapPointsMapper.MapPointToDto).ToList() : []
        };
    }
    
    public static PolygonMap DtoToPolygonMap(PolygonMapDto polygonMapDto,long userId)
    {
        return new PolygonMap
        {
            Id = 0,
            Seed = polygonMapDto.Seed,
            Height = polygonMapDto.Height,
            Width = polygonMapDto.Width,
            Polygons = polygonMapDto.Polygons,
            Name = polygonMapDto.Name,
            UserId = userId,
            HumidityPoints =
                polygonMapDto.HumidityPoints.Select(MapPointsMapper.MapPointDtoToHumidity).ToList(),
            HeightPoints = polygonMapDto.HeightPoints.Select(MapPointsMapper.MapPointDtoToHeight).ToList()
        };
    }
}