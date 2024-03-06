namespace Server.Entities.Entities;

public class Tile
{
    public long Id { get; set; }
    public string? P0 { get; set; }
    public string? P1 { get; set; }
    public string? P2 { get; set; }
    public string? P3 { get; set; }
    public TileSet? TileSet { get; set; }
    public long TileSetId { get; set; }
    public byte[]? Stream { get; set; }
}