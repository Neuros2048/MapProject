namespace Server.Entities.Entities;

public class GeneratedMap
{
    public long Id { get; set; }
    public int N { get; set; }
    public int M { get; set; }
    public string Name { get; set; }
    public long TileSetId { get; set; }
    public TileSet TileSet { get; set; }
    
    public List<SetTile> SetTiles { get; set; }
    public List<TileWeight> TileWeights { get; set; }
}