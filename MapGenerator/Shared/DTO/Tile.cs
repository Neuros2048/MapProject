﻿namespace Shared.DTO;

public class Tile
{
    public int Type { get; set; } = -1;
    public int Rotation { get; set; } = 0;
    public List<int>? Possibilities { get; set; } = new List<int>();
}