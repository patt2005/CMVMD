using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CMVMD.Shared.Data;
using CMVMD.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;

namespace CMVMD.Server.Services.User;

public class UserService : IUserService
{
    private readonly AppDbContext _appDbContext;
    private readonly TokenSettings _tokenSettings;
    private readonly IMapper _mapper;

    public UserService(AppDbContext worldDbContext,
        IOptions<TokenSettings> tokenSettings, IMapper mapper)
    {
        _appDbContext = worldDbContext;
        _tokenSettings = tokenSettings.Value;
        _mapper = mapper;
    }

    private UserDto FromUserRegistrationModelToUserModel(UserRegistrationDto userRegistration)
    {
        return new UserDto
        {
            Id = Guid.NewGuid(),
            Email = userRegistration.Email,
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            Password = userRegistration.Password,
        };
    }

    private string HashPassword(string plainPassword)
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var rfcPassord = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
        byte[] rfcPasswordHash = rfcPassord.GetBytes(20);

        byte[] passwordHash = new byte[36];
        Array.Copy(salt, 0, passwordHash, 0, 16);
        Array.Copy(rfcPasswordHash, 0, passwordHash, 16, 20);

        return Convert.ToBase64String(passwordHash);
    }

    public async Task<(bool IsUserRegistered, JWTTokenResponseDto? tokenDto)> RegisterNewUserAsync(UserRegistrationDto userRegistration)
    {
        var isUserExist = _appDbContext.Users.Any(u => u.Email.Equals(userRegistration.Email));
        if (isUserExist)
        {
            return (false, null);
        }

        var userDto = FromUserRegistrationModelToUserModel(userRegistration);
        userDto.Password = HashPassword(userDto.Password);

        var user = _mapper.Map<UserDto, Persistance.Entities.User>(userDto);

        var jwtAccessToken = GenerateJwtToken(user);

        var result = new JWTTokenResponseDto
        {
            AccessToken = jwtAccessToken,
        };

        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();
        return (true, result);
    }

    public bool CheckUserUniqueEmail(string email)
    {
        var userAlreadyExist = _appDbContext.Users.Any(_ => _.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        return !userAlreadyExist;
    }

    private bool PasswordVerification(string plainPassword, string dbPassword)
    {
        byte[] dbPasswordHash = Convert.FromBase64String(dbPassword);

        byte[] salt = new byte[16];
        Array.Copy(dbPasswordHash, 0, salt, 0, 16);

        var rfcPassord = new Rfc2898DeriveBytes(plainPassword, salt, 1000, HashAlgorithmName.SHA1);
        byte[] rfcPasswordHash = rfcPassord.GetBytes(20);

        for (int i = 0; i < rfcPasswordHash.Length; i++)
        {
            if (dbPasswordHash[i + 16] != rfcPasswordHash[i])
            {
                return false;
            }
        }
        return true;
    }

    private string GenerateJwtToken(Persistance.Entities.User user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var cliams = new List<Claim>
        {
            new Claim("Sub", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("Email", user.Email)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials,
            claims: cliams);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public async Task<(bool IsLoginSuccess, JWTTokenResponseDto? tokenResponseDto)> LoginAsync(LoginDto loginPayload)
    {
        if (string.IsNullOrEmpty(loginPayload.Email) || string.IsNullOrEmpty(loginPayload.Password))
        {
            return (false, null);
        }

        var user = await _appDbContext.Users.FirstAsync(u => u.Email == loginPayload.Email);

        if (user == null) { return (false, null); }

        bool validPassword = PasswordVerification(loginPayload.Password, user.Password);
        if (!validPassword) { return (false, null); }

        var jwtAccessToken = GenerateJwtToken(user);

        var result = new JWTTokenResponseDto
        {
            AccessToken = jwtAccessToken,
        };
        return (true, result);
    }
}
