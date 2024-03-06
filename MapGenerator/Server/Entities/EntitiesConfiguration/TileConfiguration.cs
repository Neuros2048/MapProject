using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DTO;

namespace Server.Entities.EntitiesConfiguration;

public class TileConfiguration: IEntityTypeConfiguration<Tile>
{
    public void Configure(EntityTypeBuilder<Tile> builder)
    {
        throw new NotImplementedException();
    }
}