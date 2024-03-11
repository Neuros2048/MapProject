namespace Shared.DTO;

public class TileDto
{
    public byte[]? Image { get; set; }
    //public string image { get; set; }
    public long Id { get; set; }
    public string? P0 { get; set; }
    public string? P1 { get; set; }
    public string? P2 { get; set; }
    public string? P3 { get; set; }
    public long TileSetId { get; set; }
}