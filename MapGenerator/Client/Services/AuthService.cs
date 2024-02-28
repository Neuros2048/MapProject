using System.Net.Http.Json;
using Shared.DTO;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;
using Shared.Response.SuccessRespond;

namespace Client.Services;

public class AuthService(HttpClient http)
{
    private readonly HttpClient _http = http;
    
    public async Task<string?> Login(LoginDto loginDto)
    {
        
        var result = await _http.PostAsJsonAsync("api/auth/login/", loginDto);
        if (result.IsSuccessStatusCode)
        {
            var data = await result.Content.ReadFromJsonAsync<SuccessData<string>>();
            return data!.Data;
        }
        return null;
    }

    public async Task<INotificationResult?> Register(RegisterDto registerDto)
    {
        var result = await _http.PostAsJsonAsync("api/auth/register/", registerDto);
        if (result.IsSuccessStatusCode) return new SuccessResult();
        return await result.Content.ReadFromJsonAsync<ErrorResult>();
    }

    public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var result = await _http.PutAsJsonAsync("api/auth/change-password/", changePasswordDto);
        if (result.IsSuccessStatusCode) return true;
        return false;
        
    
    }
}