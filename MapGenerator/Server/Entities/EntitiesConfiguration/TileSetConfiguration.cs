using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class TileSetConfiguration : IEntityTypeConfiguration<TileSet>
{
    public void Configure(EntityTypeBuilder<TileSet> builder)
    {
        throw new NotImplementedException();
    }
}