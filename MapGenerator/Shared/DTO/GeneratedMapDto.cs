namespace Shared.DTO;

public class GeneratedMapDto
{
    public long Id { get; set; }
    public int N { get; set; }
    public int M { get; set; }
    public int seed { get; set; }
    public string Name { get; set; }
    public long TileSetId { get; set; }
    public List<SetTileDto> SetTileDtos { get; set; }
    public List<TileWeightDto> TileWeightDtos { get; set; }
}