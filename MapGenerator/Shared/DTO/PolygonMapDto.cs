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