using Server.Services;

namespace Server.DependencyInjection;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AuthService>();
        serviceCollection.AddScoped<TileService>();
        serviceCollection.AddScoped<PolygonMapService>();
        return serviceCollection;
    }
}