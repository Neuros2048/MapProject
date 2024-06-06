namespace Shared.DTO;

public class PolygonMapDto
{
    public long Id { get; set; }
    public int Seed { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Polygons  { get; set; }
    public string? Name  { get; set; }

    public List<MapPointDto> HumidityPoints { get; set; } = [];
    public List<MapPointDto> HeightPoints { get; set; } = [];
}

public record PolygonMapDto1
(
    long Id,
    int Seed,
    int Height,
    int Width,
    int Polygons,
    string? Name,
    List<MapPointDto> HumidityPoints,
    List<MapPointDto> HeightPoints
);