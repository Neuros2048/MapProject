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
    public MapService(HttpClient http,ILocalStorageService localStorageService,IJSRuntime js)
    {
        _http = http;
        _localStorageService = localStorageService;
        _js = js;
    }
    private readonly string _controllerBase ="api/Map/";
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorageService;
    private readonly IJSRuntime _js;
    private readonly string tileKey = "tile_";
    public Dictionary<long,TileDto> TilesUrl = new Dictionary<long, TileDto>(); 

    public List<TileSetDto> TileSetDtos = new List<TileSetDto>();
  
    
    public async Task<List<TileSetDto>> GetTileSets()
    {
        var res = await _http.GetAsync(_controllerBase + "getTileSets");
        if (!res.IsSuccessStatusCode) return new List<TileSetDto>();
        return ((await res.Content.ReadFromJsonAsync<SuccessData<List<TileSetDto>>>())!).Data!;
    }
    public async Task TileSets()
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
    public async Task<bool> GetTilesToHash(long tileSetId)
    {
        var res = await _http.GetAsync(_controllerBase + "GetTiles?tileSetId="+tileSetId);
        if (!res.IsSuccessStatusCode) return false;
        foreach (var tileDto in ((await res.Content.ReadFromJsonAsync<SuccessData< List<TileDto>>>())).Data)
        {
            if (!TilesUrl.ContainsKey(tileDto.Id))
            {
                Stream stream = new MemoryStream(tileDto.Image);
                var dotnetImageStream = new DotNetStreamReference(stream);
                string url = await _js.InvokeAsync<string>("addImage", dotnetImageStream);
                tileDto.Image = null;
                tileDto.Url = url;
                TilesUrl.Add(tileDto.Id, tileDto);
            }
            else
            {
                TilesUrl[tileDto.Id].P0 = tileDto.P0;
                TilesUrl[tileDto.Id].P1 = tileDto.P1;
                TilesUrl[tileDto.Id].P2 = tileDto.P2;
                TilesUrl[tileDto.Id].P3 = tileDto.P3;
            }
            
        }

        return true;
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
    
    public async Task<bool> GetBaseTile()
    {
        
            var res = await _http.GetAsync(_controllerBase + "GetBaseTile");
            if (!res.IsSuccessStatusCode) return false!;
            var image = (await res.Content.ReadFromJsonAsync<SuccessData<TileDto>>()).Data;
            Stream stream = new MemoryStream(image.Image);
            var dotnetImageStream = new DotNetStreamReference(stream);
            string url = await _js.InvokeAsync<string>("addImage", dotnetImageStream);
            await _localStorageService.SetItemAsync(tileKey + 1, url);
        
        
        return true; 
    }

    public async Task<bool> UpDateTile(TileDto tileDto)
    {
        var res = await _http.PutAsJsonAsync(_controllerBase + "UpdateTile", tileDto);
        if (res.IsSuccessStatusCode) return true;
        return false;
    }
    
    public async Task<bool> CopyTileSet(long tileSetId)
    {
        Console.WriteLine("hejs");
        var res = await _http.GetAsync(_controllerBase + "CopyTileSet?tileId=" + tileSetId);
        Console.WriteLine("huje "+res.IsSuccessStatusCode);
        if (res.IsSuccessStatusCode) return true;
        return false;
    }
    
    public async Task<List<TileSetDto>> GetBaseTileSets()
    {
        var res = await _http.GetAsync(_controllerBase + "getBaseTileSets");
        if (!res.IsSuccessStatusCode) return new List<TileSetDto>();
        return ((await res.Content.ReadFromJsonAsync<SuccessData<List<TileSetDto>>>())!).Data!;
    }
    
    
    
}