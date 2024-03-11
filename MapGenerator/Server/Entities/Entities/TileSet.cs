namespace Server.Entities.Entities;

public class TileSet
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long UserId { get; set; } 
    public User User { get; set; }
    
    public List<Tile> Tiles { get; set; }
    public List<GeneratedMap> GeneratedMaps { get; set; }
}