using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class GeneratedMapMapper
{
    public static GeneratedMap DtoToMap(GeneratedMapDto generatedMapDto)
    {
        return new GeneratedMap
        {
            Id = 0,
            N = generatedMapDto.N,
            M = generatedMapDto.M,
            Name = generatedMapDto.Name,
            Seed = generatedMapDto.seed,
            TileSetId = generatedMapDto.TileSetId
        };
    }

    public static GeneratedMapDto MapToDto(GeneratedMap generatedMap)
    {
        return new GeneratedMapDto
        {
            Id = generatedMap.Id,
            N = generatedMap.N,
            M = generatedMap.M,
            Name = generatedMap.Name,
            seed = generatedMap.Seed,
            TileSetId = generatedMap.TileSetId
        };
    }
}