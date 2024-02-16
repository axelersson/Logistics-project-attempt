using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using LogisticsApp.Data;

[ApiController]
[Route("/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(LogisticsDBContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] User user)
    {
        _logger.LogInformation("POST request received to create user: {Username}", user.Username);

        // Hash the user's password
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.PasswordHash = hashedPassword;

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersGetAllResponse))]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(new UsersGetAllResponse { Users = users });
    }



    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUserById(string userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUser(string userId, [FromBody] User updatedUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }

        // Update user properties
        user.Username = updatedUser.Username;
        // Re-hashing the password if it's updated. 
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);

        _context.SaveChanges();

        return Ok(user);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteUser(string userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
        if (user == null)
        {
            return Unauthorized();
        }

        // Verify the password
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized();
        }

        // Authentication successful, generate JWT token
        var token = GenerateJwtToken(user);

        // Return the token
        var response = new LoginResponse
        {
            Token = token
        };
        return Ok(response);
    }

    private string GenerateJwtToken(User user)
    {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("thesecurestofkeysofchowevershouldbeintheenvdfolder"); // JWT Token key

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Role, user.Role.ToString()), // Convert enum to string
            // Add other claims as needed
        }),
        Expires = DateTime.UtcNow.AddHours(10), // Set token expiration
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
    }

 
    [HttpGet("getAllUsernames")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersGetAllUsernamesResponse))]
    public async Task<IActionResult> GetAllUsernames()
    {
        var usernamesList = await _context.Users
            .Select(u => u.Username)
            .ToListAsync();

        var response = new UsersGetAllUsernamesResponse
        {
            Usernames = usernamesList
        };

        return Ok(response);
    }
}
