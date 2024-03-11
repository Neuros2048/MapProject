namespace Server.Entities.Entities;

public class SetTile
{
    public long Id { get; set; }
    public int N { get; set; }
    public int M { get; set; }
    public long GeneratedMapId { get; set; }
    public GeneratedMap GeneratedMap { get; set; }
    public long TileId { get; set; }
    public Tile Tile { get; set; }
        
}