using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class TileWeightConfiguration : IEntityTypeConfiguration<TileWeight>
{
    public void Configure(EntityTypeBuilder<TileWeight> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.GeneratedMap)
            .WithMany(x => x.TileWeights)
            .HasForeignKey(x => x.GeneratedMapId);

        builder.HasOne(x => x.Tile)
            .WithMany(x => x.TileWeights)
            .HasForeignKey(x => x.TileId).
            OnDelete(DeleteBehavior.NoAction);
    }
}