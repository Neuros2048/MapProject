using Server.Entities.Entities;
using Shared.DTO;

namespace Server.Entities.Mappers;

public static class UserMapper
{
    public static User RegisterToUser(RegisterDto registerDto,int workFactor = 4)
    {
        return new User()
        {
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, workFactor),
            Username = registerDto.Username
        };
    }
}