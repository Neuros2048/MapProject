namespace Server.Entities.Entities;

public abstract class MapPoints
{
    public long Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public double Power { get; set; }
        
    public long PolygonMapId { get; set; }
    public PolygonMap PolygonMap { get; set; }
}