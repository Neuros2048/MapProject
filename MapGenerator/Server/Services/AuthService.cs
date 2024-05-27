using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Entities.Entities;
using Server.Entities.Mappers;
using Server.Models;
using Server.Response;
using Shared.DTO;
using Shared.Response.ErrorRespond;
using Shared.Response.IResponse;
using Shared.Response.SuccessRespond;

namespace Server.Services;

public class AuthService(DataContext context, IConfiguration configuration)
{
    private readonly int workFacktor = 4;


    public async Task<HandlerResult<Success,IErrorResult>> Register(RegisterDto registerDto)
    {
        var email = await context.Users.FirstOrDefaultAsync(x => x.Email.Equals(registerDto.Email));
        
        if (email != null) return new IncorrectData();
        User user = UserMapper.RegisterToUser(registerDto, workFacktor);
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return new Success();
    }
    
    
    public async Task<HandlerResult<SuccessData<string>,IErrorResult>> Login(LoginDto loginDto)
    {

        var user = await context.Users.FirstOrDefaultAsync(x => x.Email.Equals(loginDto.Email));
        if (user == null) return new EntityNotFound();
        bool ans = VerifyPasswordHash(loginDto.Password, user.PasswordHash);
        if (!ans) return new WrongPassword();

        return new SuccessData<string>()
        {
            Data = CreateToken(user)
        };

    }
    
    public async Task<HandlerResult<Success,IErrorResult>> ChangePassword(ChangePasswordDto changePasswordDto, int userId)
    {
        if (!changePasswordDto.NewPassword.Equals(changePasswordDto.ConfirmPassword)) return new IncorrectData();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return new EntityNotFound();
            bool ans = VerifyPasswordHash(changePasswordDto.LastPassword, user.PasswordHash);
            if (!ans) return new WrongPassword();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword, workFacktor);
            await context.SaveChangesAsync();
            return new Success();
    }

   

    private bool VerifyPasswordHash(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
         {
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
             new Claim(ClaimTypes.Name, user.Username),
         };


        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Token").Value));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                           claims: claims,
                           expires: DateTime.Now.AddHours(3),
        signingCredentials: creds
              );

        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenHandler;
    }

    
}