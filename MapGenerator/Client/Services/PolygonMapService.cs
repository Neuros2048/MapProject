using System.Net.Http.Json;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using Blazored.LocalStorage;
using Client.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using Shared.DTO;
using Shared.Response.SuccessRespond;

namespace Client.Services;

public class PolygonMapService
{
    public PolygonMapService(HttpClient http,ILocalStorageService localStorageService,IJSRuntime js)
    {
        _http = http;
        _localStorageService = localStorageService;
        _js = js;
    }
    private readonly string _controllerBase ="api/Map/";
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorageService;
    private readonly IJSRuntime _js;
   
    public async Task<List<PolygonMapDto>> GetAllPolygonMaps()
    {
        var res = await _http.GetAsync(_controllerBase + "getAllPolygonMaps");
        if (!res.IsSuccessStatusCode) return new List<PolygonMapDto>();
        return (await res.Content.ReadFromJsonAsync<SuccessData<List<PolygonMapDto>>>())!.Data!;
    }
    public async Task<PolygonMapDto> GetPolygonMap(long mapId)
    {
        var res = await _http.GetAsync(_controllerBase + "getPolygonMap?mapId=" + mapId);
        if (!res.IsSuccessStatusCode) return null!;
        return (await res.Content.ReadFromJsonAsync<SuccessData<PolygonMapDto>>())!.Data!;
    }
    public async Task<bool> DeletePolygonMap(long mapId)
    {
        var res = await _http.DeleteAsync(_controllerBase + "DeletePolygonMap?mapId="+mapId);
        if (!res.IsSuccessStatusCode) return false;
        return true;
    }
    public async Task<bool> CreatPolygonMap(PolygonMapDto polygonMapDto)
    {
        var res = await _http.PutAsJsonAsync(_controllerBase + "CreatPolygonMap",polygonMapDto);
        if (!res.IsSuccessStatusCode) return false;
        return true;
    }
  

    

    

}