﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.EntitiesConfiguration;

public class TileConfiguration: IEntityTypeConfiguration<Tile>
{
    public void Configure(EntityTypeBuilder<Tile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.TileSet)
            .WithMany(x => x.Tiles)
            .HasForeignKey(x => x.TileSetId);
        builder.Property(x => x.Stream)
            .IsRequired();
    }
}