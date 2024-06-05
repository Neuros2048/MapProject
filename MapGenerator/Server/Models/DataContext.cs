using Microsoft.EntityFrameworkCore;
using Server.Entities.Entities;

namespace Server.Models;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options){
    public DbSet<User> Users { get; set; }
    public DbSet<TileSet> TileSets { get; set; }
    public DbSet<Tile> Tiles { get; set; }
    public DbSet<GeneratedMap> GeneratedMaps { get; set; }
    public DbSet<SetTile> SetTiles { get; set; }
    public DbSet<TileWeight> Weights { get; set; }
    public DbSet<PolygonMap> PolygonMaps { get; set; }
    public DbSet<HumidityPoint> HumidityPoints { get; set; }
    public DbSet<HeightPoint> HeightPoints { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TileSet).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Tile).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneratedMap).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SetTile).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TileWeight).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PolygonMap).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeightPoint).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HumidityPoint).Assembly);

        Seed(modelBuilder);
    }
    
    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(Seeder.GenerateUsers());
        modelBuilder.Entity<TileSet>().HasData(Seeder.GenerateTileSets());
        modelBuilder.Entity<Tile>().HasData(Seeder.GenerateTiles());
    }
}