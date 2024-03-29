using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
public interface ITokenService
{
    bool IsAdmin(string token);
    string GenerateJwtToken(User user);
    bool VerifyToken(string token);
    string GetUsernameFromToken(string token); // Add this line
}

public class TokenService : ITokenService
{
    private readonly string _jwtSecretKey;

    public TokenService(IConfiguration configuration)
    {
        _jwtSecretKey = configuration.GetValue<string>("JWT_Secret")!;


        if (string.IsNullOrEmpty(_jwtSecretKey))
        {
            throw new InvalidOperationException("JWT_Secret configuration is not set.");
        }
    }

    public bool IsAdmin(string token)
    {
        if (_jwtSecretKey == null)
        {
            throw new InvalidOperationException("JWT_Secret is null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Clock skew compensates for server time drift.
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var role = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            return role == "Admin";
        }
        catch
        {
            // Token validation failed
            return false;
        }
    }

    public string GenerateJwtToken(User user)
    {
        if (_jwtSecretKey == null)
        {
            throw new InvalidOperationException("JWT_Secret is null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Role, user.Role.ToString()), // Convert enum to string
                new Claim(ClaimTypes.Name, user.Username),


                // Add other claims as needed
            }),
            Expires = DateTime.UtcNow.AddHours(10), // Set token expiration
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool VerifyToken(string token)
    {
        if (_jwtSecretKey == null)
        {
            throw new InvalidOperationException("JWT_Secret is null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Clock skew compensates for server time drift.
                ClockSkew = TimeSpan.Zero
            }, out _);

            // Token is valid
            return true;
        }
        catch
        {
            // Token validation failed
            return false;
        }
    }
    public string GetUsernameFromToken(string token)
    {
        if (_jwtSecretKey == null)
        {
            throw new InvalidOperationException("JWT_Secret is null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // Clock skew compensates for server time drift.
            }, out SecurityToken validatedToken);

            var usernameClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(usernameClaim))
            {
                // Handle the case where the username claim does not exist or is not set
                throw new InvalidOperationException("Username claim not found in token.");
            }

            return usernameClaim;
        }
        catch (Exception ex)
        {
            // Token validation failed or another error occurred
            throw new InvalidOperationException($"Error retrieving username from token: {ex.Message}");
        }
    }
}
