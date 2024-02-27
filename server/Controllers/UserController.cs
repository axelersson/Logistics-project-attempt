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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ILogger<UsersController> _logger;

    private readonly ITokenService _tokenService;

    public UsersController(LogisticsDBContext context, ILogger<UsersController> logger, ITokenService tokenService)
    {
        _context = context;
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] UserCreateModel user, [FromHeader(Name = "Authorization")] string token)
    {   
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", ""))) // Assuming bearer token is used
        {
            return Unauthorized("Only admins can create users.");
        }

        // Hash the user's password
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        // Create a new User object from the UserCreateModel
        var newUser = new User
        {
            Username = user.Username,
            PasswordHash = hashedPassword,
            Role = user.Role
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();
        _logger.LogInformation("Creating user: {Username}", user.Username);

        return Ok(newUser);
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

    [HttpGet("ByUsername/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserIdAndRoleResponseFromUsernameRequest))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUserByUsername(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return NotFound();
        }

        // Pass the username along with UserId and Role to the response model
        var response = new UserIdAndRoleResponseFromUsernameRequest(user.UserId, user.Username, user.Role);
        return Ok(response);
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
    [HttpPut("/updaterole/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUserRole(string userId, [FromBody] UpdateUserRoleModel updatedUserRole)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == updatedUserRole.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Update user role
        user.Role = updatedUserRole.Role;

        _context.SaveChanges();

        return Ok(user);
    }
    [HttpPut("updatepasswordandrole/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUserPasswordAndRole(string userId, [FromBody] UpdateUserPasswordAndRoleModel updatedUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Update user properties
        user.Role = updatedUser.Role;
        // Re-hashing the password if it's updated. 
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.NewPassword);

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
        var token = _tokenService.GenerateJwtToken(user);

        // Return the token
        var response = new LoginResponse
        {
            Token = token
        };
        return Ok(response);
    }
 
    [HttpGet("getAllUsernames")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersGetAllUsernamesResponse))]
    [Authorize] // Add the [Authorize] attribute to require authentication
    public async Task<IActionResult> GetAllUsernames()
    {
        // Extract JWT token from the request headers
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        // Verify the JWT token
        if (string.IsNullOrEmpty(token) || !_tokenService.VerifyToken(token))
        {
            return Unauthorized(); // Return 401 Unauthorized if the token is missing or invalid
        }

        // Proceed with retrieving usernames if the token is valid

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
