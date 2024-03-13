﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240313120406_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.Entities.Entities.GeneratedMap", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("M")
                        .HasColumnType("int");

                    b.Property<int>("N")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TileSetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TileSetId");

                    b.ToTable("GeneratedMaps");
                });

            modelBuilder.Entity("Server.Entities.Entities.SetTile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GeneratedMapId")
                        .HasColumnType("bigint");

                    b.Property<int>("M")
                        .HasColumnType("int");

                    b.Property<int>("N")
                        .HasColumnType("int");

                    b.Property<long>("TileId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GeneratedMapId");

                    b.HasIndex("TileId");

                    b.ToTable("SetTiles");
                });

            modelBuilder.Entity("Server.Entities.Entities.Tile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("P0")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Stream")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Symmetry")
                        .HasColumnType("int");

                    b.Property<long>("TileSetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TileSetId");

                    b.ToTable("Tiles");
                });

            modelBuilder.Entity("Server.Entities.Entities.TileSet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TileSets");
                });

            modelBuilder.Entity("Server.Entities.Entities.TileWeight", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GeneratedMapId")
                        .HasColumnType("bigint");

                    b.Property<long>("TileId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GeneratedMapId");

                    b.HasIndex("TileId");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("Server.Entities.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Entities.Entities.GeneratedMap", b =>
                {
                    b.HasOne("Server.Entities.Entities.TileSet", "TileSet")
                        .WithMany("GeneratedMaps")
                        .HasForeignKey("TileSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TileSet");
                });

            modelBuilder.Entity("Server.Entities.Entities.SetTile", b =>
                {
                    b.HasOne("Server.Entities.Entities.GeneratedMap", "GeneratedMap")
                        .WithMany("SetTiles")
                        .HasForeignKey("GeneratedMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entities.Entities.Tile", "Tile")
                        .WithMany("SetTiles")
                        .HasForeignKey("TileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GeneratedMap");

                    b.Navigation("Tile");
                });

            modelBuilder.Entity("Server.Entities.Entities.Tile", b =>
                {
                    b.HasOne("Server.Entities.Entities.TileSet", "TileSet")
                        .WithMany("Tiles")
                        .HasForeignKey("TileSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TileSet");
                });

            modelBuilder.Entity("Server.Entities.Entities.TileSet", b =>
                {
                    b.HasOne("Server.Entities.Entities.User", "User")
                        .WithMany("TileSets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Entities.Entities.TileWeight", b =>
                {
                    b.HasOne("Server.Entities.Entities.GeneratedMap", "GeneratedMap")
                        .WithMany("TileWeights")
                        .HasForeignKey("GeneratedMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entities.Entities.Tile", "Tile")
                        .WithMany("TileWeights")
                        .HasForeignKey("TileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GeneratedMap");

                    b.Navigation("Tile");
                });

            modelBuilder.Entity("Server.Entities.Entities.GeneratedMap", b =>
                {
                    b.Navigation("SetTiles");

                    b.Navigation("TileWeights");
                });

            modelBuilder.Entity("Server.Entities.Entities.Tile", b =>
                {
                    b.Navigation("SetTiles");

                    b.Navigation("TileWeights");
                });

            modelBuilder.Entity("Server.Entities.Entities.TileSet", b =>
                {
                    b.Navigation("GeneratedMaps");

                    b.Navigation("Tiles");
                });

            modelBuilder.Entity("Server.Entities.Entities.User", b =>
                {
                    b.Navigation("TileSets");
                });
#pragma warning restore 612, 618
        }
    }
}