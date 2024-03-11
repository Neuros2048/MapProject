using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class TileSetConfiguration : IEntityTypeConfiguration<TileSet>
{
    public void Configure(EntityTypeBuilder<TileSet> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.TileSets)
            .HasForeignKey(x => x.UserId);
    }
}