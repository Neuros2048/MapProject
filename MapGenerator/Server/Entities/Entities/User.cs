namespace Server.Entities.Entities;

public class User
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? Email { get; set; }
    
    public List<TileSet> TileSets { get; set; }
    public List<PolygonMap> PolygonMaps { get; set; }
}