namespace Server.Entities.Entities;

public class TileWeight
{
    public long Id { get; set; }
    public int Weight;
    public long GeneratedMapId { get; set; }
    public GeneratedMap GeneratedMap { get; set; }
    public long TileId { get; set; }
    public Tile Tile { get; set; }
}