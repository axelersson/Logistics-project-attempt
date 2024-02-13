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
using System.Security.Cryptography;
using LogisticsApp.Data;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LogisticsDBContext _context; // Replace with the actual name of your DbContext
    private readonly ILogger<UsersController> _logger; // Logger instance

    public UsersController(LogisticsDBContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _logger.LogInformation("POST request received to create user: {Username}", user.Username);

        // Hash the user's password using BCrypt
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.PasswordHash = hashedPassword;

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{userId}")]
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
    public IActionResult UpdateUser(string userId, [FromBody] User updatedUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }

        // Update user properties here
        user.Username = updatedUser.Username;
        user.PasswordHash = updatedUser.PasswordHash;

        _context.SaveChanges();

        return Ok(user);
    }

    [HttpDelete("{userId}")]
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
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
    if (user == null)
    {
        return Unauthorized();
    }

    // Verify the password using BCrypt
    if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
    {
        return Unauthorized();
    }

    // Authentication successful, generate JWT token
    var token = GenerateJwtToken(user);

    // Return the token using the LoginResponse model
    var response = new LoginResponse
    {
        Token = token
    };
    return Ok(response);
    }
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("testingthissecretkeydforthedotnetproject"); // Change this to a secure secret key

        // Calculate token expiration to be 10 hours from now
    var tokenExpiration = DateTime.UtcNow.AddHours(10);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.UserId),
        new Claim(ClaimTypes.Role, user.Role.ToString()) // Assuming user.Role is enum UserRole
        }),
        Expires = tokenExpiration, // Token expiration set to 10 hours from now
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    // GET: /users/getAllUsernames
        [HttpGet("getAllUsernames")]
        public async Task<IActionResult> GetAllUsernames()
        {
            var usernames = await _context.Users
                .Select(u => u.Username)
                .ToListAsync();

            return Ok(usernames);
        }
}

