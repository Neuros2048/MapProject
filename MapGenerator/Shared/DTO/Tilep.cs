namespace Shared.DTO;

public class Tilep
{
    public long Type { get; set; } = -1;
    public int WeightSum { get; set; } = 0;

    public List<int>? Possibilities { get; set; } = null;
}