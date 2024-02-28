using Microsoft.EntityFrameworkCore;
using Server.Entities.Entities;

namespace Server.Models;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options){
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
    }
    
    private static void Seed(ModelBuilder modelBuilder)
    {
    }
}