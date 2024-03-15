namespace Shared.DTO;

public class Tilep
{
    public long Type { get; set; } = -1;

    public List<int>? Possibilities { get; set; } = new List<int>();
}