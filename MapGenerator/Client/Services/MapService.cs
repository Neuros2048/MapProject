﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Shared.DTO;
using Shared.Response.SuccessRespond;

namespace Client.Services;

public class MapService
{
    public MapService(HttpClient http,ILocalStorageService localStorageService)
    {
        _http = http;
        _localStorageService = localStorageService;
    }
    private readonly string _controllerBase ="api/Map/";
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorageService;

    public List<TileSetDto> TileSetDtos;
    public async Task<byte[]> SendImageToServer(string url)
    {
        // Convert byte array to base64 string
        var response = await _http.GetAsync(url);
        byte[] imageData = await response.Content.ReadAsByteArrayAsync();
        Console.WriteLine(Convert.ToBase64String(imageData));
        TileDto tileDto = new TileDto()
        {
            Id = 0,
            Image = imageData
           // image = Convert.ToBase64String(imageData)
        };
        var r1 = await _http.GetAsync("api/Map/get");
        Console.WriteLine(r1.IsSuccessStatusCode);
        
        var result = await _http.PostAsJsonAsync("api/Map/Add/",tileDto );
        Console.WriteLine("a");
        Console.WriteLine(result.IsSuccessStatusCode);
        var r2 = await result.Content.ReadFromJsonAsync<TileDto>();
        Console.WriteLine(r2.Image);
        byte[] fileBytes = r2.Image;//Convert.FromBase64String(r2.image);
        return fileBytes;
        // Send the base64 string to the server
    }
    
    public async Task<List<TileSetDto>> TileSets()
    {
        var res = await _http.GetAsync(_controllerBase + "getTileSets");
        if (!res.IsSuccessStatusCode) return new List<TileSetDto>();
        return ((await res.Content.ReadFromJsonAsync<SuccessData<List<TileSetDto>>>())!).Data!;
    }
    public async Task TileSets2()
    {
        var res = await _http.GetAsync(_controllerBase + "getTileSets");
        if (!res.IsSuccessStatusCode) return;
        TileSetDtos =  ((await res.Content.ReadFromJsonAsync<SuccessData<List<TileSetDto>>>())!).Data!;
    }
    public async Task<TileDto> AddTile(TileDto tileDto, string url)
    {
        var response = await _http.GetAsync(url);
        byte[] imageData = await response.Content.ReadAsByteArrayAsync();
        tileDto.Image = imageData;
        var res = await _http.PutAsJsonAsync(_controllerBase+"addTileToSet" , tileDto);
        if (!res.IsSuccessStatusCode) return null!;
        return ((await res.Content.ReadFromJsonAsync<SuccessData< TileDto >>())!).Data!;
         
    }
    
    
    public async Task<List<TileDto>> GetTiles(long tileSetId)
    {
        var res = await _http.GetAsync(_controllerBase + "GetTiles?tileSetId="+tileSetId);
        if (!res.IsSuccessStatusCode) return null!;
        
        return ((await res.Content.ReadFromJsonAsync<SuccessData< List<TileDto>>>())!).Data!; 
    }
    
    public async Task<TileSetDto?> CreateTileSet(TileSetDto tileSetDto)
    {
        var res = await _http.PutAsJsonAsync(_controllerBase + "CreatTileSet", tileSetDto);
        if (!res.IsSuccessStatusCode) return null;
        return ((await res.Content.ReadFromJsonAsync<SuccessData< TileSetDto >>())!).Data!; 
         
    }

    public async Task<bool> DeleteTileSet(long tileSetId)
    {
        var res = await _http.DeleteAsync(_controllerBase + "DeleteTileSet?tileSetId=" + tileSetId);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTile(long tileId)
    {
        var res = await _http.DeleteAsync(_controllerBase + "DeleteTile?tileId=" + tileId);
        return res.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateTile(TileDto tileDto)
    {
        var res = await _http.PostAsJsonAsync(_controllerBase + "UpdateTile", tileDto);
        return res.IsSuccessStatusCode;

       
    }
    
    
    public async Task<GeneratedMapDto> PrepareNewMap(long tileSetId)
    { 
        var res = await _http.GetAsync(_controllerBase + "PrepareNewMap?tileSetId=" + tileSetId);
        if (!res.IsSuccessStatusCode) return null!;
        return ((await res.Content.ReadFromJsonAsync<SuccessData< GeneratedMapDto >>())!).Data!; 
    }
    
    public async Task<bool> SaveGeneratedMap(GeneratedMapDto mapDto)
    {
        var res = await _http.PutAsJsonAsync(_controllerBase + "SaveGeneratedMap", mapDto);
        return res.IsSuccessStatusCode;
    }
    
    
    public async Task<List<GeneratedMapDto>> GetMaps(long tileSetId)
    {
        var res = await _http.GetAsync(_controllerBase + "GetMaps?tileSetId=" + tileSetId);
        if (!res.IsSuccessStatusCode) return null!;
        return ((await res.Content.ReadFromJsonAsync<SuccessData<List< GeneratedMapDto >>>())!).Data!; 
    }
    
    public async Task<GeneratedMapDto> GetMap(long mapId)
    {
        var res = await _http.GetAsync(_controllerBase + "GetMap?mapId=" + mapId);
        if (!res.IsSuccessStatusCode) return null!;
        return ((await res.Content.ReadFromJsonAsync<SuccessData<GeneratedMapDto>>())!).Data!; 
    }
    
    public async Task<TileDto> GetBaseTile()
    {
        var res = await _http.GetAsync(_controllerBase + "GetBaseTile");
        if (!res.IsSuccessStatusCode) return null!;
        return ((await res.Content.ReadFromJsonAsync<SuccessData<TileDto>>())!).Data!; 
    }
    
    
}