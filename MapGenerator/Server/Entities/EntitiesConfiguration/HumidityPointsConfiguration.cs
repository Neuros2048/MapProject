using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;

namespace Server.Entities.EntitiesConfiguration;

public class HumidityPointsConfiguration : IEntityTypeConfiguration<HumidityPoint>
{
    public void Configure(EntityTypeBuilder<HumidityPoint> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Power).IsRequired();
        builder.Property(x => x.X).IsRequired();
        builder.Property(x => x.Y).IsRequired();
        
        builder.HasOne(x => x.PolygonMap)
            .WithMany(x => x.HumidityPoints)
            .HasForeignKey(x => x.PolygonMapId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}