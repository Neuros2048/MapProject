using Server.Entities.Entities;

namespace Server.Models;

public static class Seeder
{
    public static IEnumerable<User> GenerateUsers()
    {
        return new List<User>()
        {
            new User
            {
                Id = 1,
                Username ="aa",
                PasswordHash = "aa",
                Email = "aa",
            }
        };
    }

    public static IEnumerable<TileSet> GenerateTileSets()
    {
        return new List<TileSet>()
        {
            new TileSet
            {
                Id = 1,
                Name = "a",
                UserId = 1,
            }
        };
    }

    public static IEnumerable<Tile> GenerateTiles()
    {
        return new List<Tile>
        {
            new Tile()
            {
                Id = 1, P0 = "0", P1 = "0", P2 = "0", P3 = "0",Stream =Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII="),
                TileSetId = 1
            }
        };
    }
    
        
}