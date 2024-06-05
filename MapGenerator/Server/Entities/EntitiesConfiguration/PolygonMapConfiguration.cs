using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class PolygonMapConfiguration: IEntityTypeConfiguration<PolygonMap>
{
    public void Configure(EntityTypeBuilder<PolygonMap> builder)
    {
        builder.HasKey(x => x.Id);
        

        builder.Property(x => x.Seed ).IsRequired();
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Width).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.PolygonMaps)
            .HasForeignKey(x => x.UserId);
    }
}