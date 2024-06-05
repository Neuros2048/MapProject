namespace Server.Entities.Entities;

public class PolygonMap
{
    
    public long Id { get; set; }
    public int Seed { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Polygons { get; set; }
    public string? Name  { get; set; }
    public long UserId { get; set; } 
    public User User { get; set; }
    
    public List<HumidityPoint>? HumidityPoints { get; set; }
    public List<HeightPoint>? HeightPoints { get; set; }
}