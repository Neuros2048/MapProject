using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using Shared.DTO;

namespace Client.Services;

public class MapService
{
    public MapService(HttpClient http)
    {
        _http = http;
    }

    private readonly HttpClient _http;
    public async Task<byte[]> SendImageToServer(string url)
    {
        // Convert byte array to base64 string
        var response = await _http.GetAsync(url);
        byte[] imageData = await response.Content.ReadAsByteArrayAsync();
        Console.WriteLine(Convert.ToBase64String(imageData));
        TileDto tileDto = new TileDto()
        {
            id = 0,
            image = Convert.ToBase64String(imageData)
        };
        var r1 = await _http.GetAsync("api/Map/get");
        Console.WriteLine(r1.IsSuccessStatusCode);
        
        var result = await _http.PostAsJsonAsync("api/Map/Add/",tileDto );
        Console.WriteLine("a");
        Console.WriteLine(result.IsSuccessStatusCode);
        var r2 = await result.Content.ReadFromJsonAsync<TileDto>();
        Console.WriteLine(r2.image);
        byte[] fileBytes = Convert.FromBase64String(r2.image);
        return fileBytes;
        // Send the base64 string to the server
    }
}