using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class GeneratedMapConfiguration : IEntityTypeConfiguration<GeneratedMap>
{
    public void Configure(EntityTypeBuilder<GeneratedMap> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.TileSet)
            .WithMany(x => x.GeneratedMaps)
            .HasForeignKey(x => x.TileSetId);
    }
}