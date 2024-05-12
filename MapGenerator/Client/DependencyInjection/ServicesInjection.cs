using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.DependencyInjection;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AuthService>();
        serviceCollection.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        serviceCollection.AddScoped<ImageEditor>();
        serviceCollection.AddScoped<MapService>();
        serviceCollection.AddScoped<PolygonMapService>();
        return serviceCollection;
    }
}