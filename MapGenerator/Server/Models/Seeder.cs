using System.Drawing;
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
            },
            new TileSet
            {
                Id = 2,
                Name = "Carcasonne",
                UserId = 1,
            },
            new TileSet
            {
                Id = 3,
                Name = "pipes",
                UserId = 1,
            }
            
            
        };
    }

    public static IEnumerable<Tile> GenerateTiles()
    {
        
        
        var list = new List<Tile>
        {
            new Tile()
            {
                Id = 1, P0 = "0", P1 = "0", P2 = "0", P3 = "0",Stream =Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAADpJREFUKFPt0KERADAIBMF/j0JB/wXRCS08M/EkDeT0qqO7q7vxigAk6epI4sN10dkTEcpMVNUKzQwDWXAoJWfFnuMAAAAASUVORK5CYII="),
                TileSetId = 1
            },
            new Tile()
            {
                Id = 1+1, P0 = "0", P1 = "0", P2 = "0", P3 = "0",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+2, P0 = "1", P1 = "1", P2 = "0", P3 = "0",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+3, P0 = "0", P1 = "1", P2 = "1", P3 = "0",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+4, P0 = "0", P1 = "0", P2 = "1", P3 = "1",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+5, P0 = "1", P1 = "0", P2 = "0", P3 = "1",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+6, P0 = "1", P1 = "1", P2 = "1", P3 = "1",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+7, P0 = "0", P1 = "1", P2 = "0", P3 = "1",
                TileSetId = 3
            },
            new Tile()
            {
                Id = 1+8, P0 = "1", P1 = "0", P2 = "1", P3 = "0",
                TileSetId = 3
            }
            /*new Tile()
            {
                Id = 2, P0 = "111", P1 = "200", P2 = "002", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 3, P0 = "131", P1 = "200", P2 = "002", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 4, P0 = "202", P1 = "111", P2 = "202", P3 = "111",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 5, P0 = "111", P1 = "202", P2 = "202", P3 = "111",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 6, P0 = "111", P1 = "202", P2 = "111", P3 = "202",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 7, P0 = "111", P1 = "202", P2 = "111", P3 = "111",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 8, P0 = "111", P1 = "202", P2 = "111", P3 = "111",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 9, P0 = "111", P1 = "202", P2 = "131", P3 = "131",
                TileSetId = 2
            }
            ,
            new Tile()
            {
                Id = 10, P0 = "131", P1 = "202", P2 = "111", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 11, P0 = "131", P1 = "202", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 12, P0 = "131", P1 = "202", P2 = "131", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 13, P0 = "111", P1 = "131", P2 = "111", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 14, P0 = "111", P1 = "111", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 15, P0 = "111", P1 = "111", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 16, P0 = "131", P1 = "111", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 17, P0 = "131", P1 = "131", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 18, P0 = "111", P1 = "111", P2 = "111", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 19, P0 = "111", P1 = "111", P2 = "111", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 20, P0 = "200", P1 = "000", P2 = "002", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 21, P0 = "111", P1 = "202", P2 = "111", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 22, P0 = "131", P1 = "131", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 23, P0 = "131", P1 = "111", P2 = "131", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 24, P0 = "131", P1 = "200", P2 = "002", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 25, P0 = "202", P1 = "111", P2 = "111", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 26, P0 = "202", P1 = "202", P2 = "202", P3 = "202",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 27, P0 = "000", P1 = "000", P2 = "000", P3 = "000",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 28, P0 = "131", P1 = "111", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 29, P0 = "131", P1 = "111", P2 = "131", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 30, P0 = "111", P1 = "202", P2 = "131", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 31, P0 = "111", P1 = "200", P2 = "002", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 32, P0 = "111", P1 = "202", P2 = "202", P3 = "202",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 33, P0 = "202", P1 = "131", P2 = "202", P3 = "131",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 34, P0 = "131", P1 = "202", P2 = "111", P3 = "111",
                TileSetId = 2
            },
            new Tile()
            {
                Id = 35, P0 = "131", P1 = "131", P2 = "131", P3 = "131",
                TileSetId = 2
            },new Tile()
            {
                Id = 36, P0 = "202", P1 = "131", P2 = "131", P3 = "111",
                TileSetId = 2
            }*/
            
            
            
            
        };
        /*for(int i = 1; i <= 35 ; i++)
        {
            

            using (Image image = Image.FromFile($"Models/Images/C{i}.png"))
            {
                using (MemoryStream m = new MemoryStream())
                { 
                    image.Save(m, image.RawFormat);
                   list[i].Stream = m.ToArray();

                }
            }
        }*/
        for(int i = 1; i <= 8 ; i++)
        {
            

            using (Image image = Image.FromFile($"Models/Images/k/k{i}.png"))
            {
                using (MemoryStream m = new MemoryStream())
                { 
                    image.Save(m, image.RawFormat);
                    list[i].Stream = m.ToArray();

                }
            }
        }
        return list;
       
    }
    
        
}