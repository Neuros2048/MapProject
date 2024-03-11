using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class SetTileConfiguration : IEntityTypeConfiguration<SetTile>
{
    public void Configure(EntityTypeBuilder<SetTile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.GeneratedMap)
            .WithMany(x => x.SetTiles)
            .HasForeignKey(x => x.GeneratedMapId);

        builder.HasOne(x => x.Tile)
            .WithMany(x => x.SetTiles)
            .HasForeignKey(x => x.TileId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}