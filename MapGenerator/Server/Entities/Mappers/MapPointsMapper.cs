using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class MapPointsMapper
{
     public static HumidityPoint MapPointDtoToHumidity(MapPointDto mapPointDto)
     {
          return new HumidityPoint
          {
               Id = 0,
               X = mapPointDto.X,
               Y = mapPointDto.Y,
               Power = mapPointDto.Power,
               PolygonMapId = 0,
               PolygonMap = null
          };
     }
     
     public static HeightPoint MapPointDtoToHeight(MapPointDto mapPointDto)
     {
          return new HeightPoint()
          {
               Id = 0,
               X = mapPointDto.X,
               Y = mapPointDto.Y,
               Power = mapPointDto.Power,
               PolygonMapId = 0,
               PolygonMap = null
          };
     }

     public static MapPointDto MapPointToDto(MapPoints mapPoints)
     {
          return new MapPointDto
          {
               X = mapPoints.X,
               Y = mapPoints.Y,
               Power = mapPoints.Power
          };
     }
}