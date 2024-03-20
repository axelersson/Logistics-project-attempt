using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApp.Data;
using System.Security.Claims;

[ApiController]
[Route("/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LogisticsDBContext _context;
    private readonly ITokenService _tokenService;
    private readonly ILoggerService _loggingService;

    public UsersController(LogisticsDBContext context, ITokenService tokenService, ILoggerService loggingService)
    {
        _context = context;
        _tokenService = tokenService;
        _loggingService = loggingService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateModel user, [FromHeader(Name = "Authorization")] string token)
    {   
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can create users.");
        }

        var adminUsername = _tokenService.GetUsernameFromToken(token.Replace("Bearer ", ""));
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = new User
        {
            Username = user.Username,
            PasswordHash = hashedPassword,
            Role = user.Role
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        _loggingService.LogInformation($"User {newUser.Username} was created by {adminUsername}.");

        return Ok(newUser);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can view users.");
        }

        var users = await _context.Users.ToListAsync();
        return Ok(new UsersGetAllResponse { Users = users });
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(string userId)
    {
        // TEMPORARY DISABLE FOR DEMO PURPOSES

        // if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        // {
        //     return Unauthorized("Only admins can view user details.");
        // }

        var user = _context.Users.Include(t => t.TruckUsers.Where(t => t.IsAssigned == true)).FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("ByUsername/{username}")]
    public IActionResult GetUserByUsername(string username, [FromHeader(Name = "Authorization")] string token)
    {

        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can view user details.");
        }

        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(new UserIdAndRoleResponseFromUsernameRequest(user.UserId, user.Username, user.Role));
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(string userId, [FromBody] User updatedUser, [FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can update users.");
        }

        var adminUsername = _tokenService.GetUsernameFromToken(token.Replace("Bearer ", ""));
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.Username = updatedUser.Username;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
        _context.SaveChanges();

        _loggingService.LogInformation($"User {user.Username} was updated by {adminUsername}.");

        return Ok(user);
    }

    [HttpPut("/updaterole/{userId}")]
    public IActionResult UpdateUserRole(string userId, [FromBody] UpdateUserRoleModel updatedUserRole, [FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can update user roles.");
        }

        var adminUsername = _tokenService.GetUsernameFromToken(token.Replace("Bearer ", ""));
        var user = _context.Users.FirstOrDefault(u => u.UserId == updatedUserRole.UserId);
        if (user == null)
        {
            return NotFound();
        }

        user.Role = updatedUserRole.Role;
        _context.SaveChanges();

        _loggingService.LogInformation($"User role for {user.Username} was updated by {adminUsername}.");

        return Ok(user);
    }

    [HttpPut("updatepasswordandrole/{userId}")]
    public IActionResult UpdateUserPasswordAndRole(string userId, [FromBody] UpdateUserPasswordAndRoleModel updatedUser, [FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can update user passwords and roles.");
        }

        var adminUsername = _tokenService.GetUsernameFromToken(token.Replace("Bearer ", ""));
        var user = _context.Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
        if (user == null)
        {
            return NotFound();
        }

        user.Role = updatedUser.Role;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.NewPassword);
        _context.SaveChanges();

        _loggingService.LogInformation($"User password and role for {user.Username} were updated by {adminUsername}.");

        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(string userId, [FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can delete users.");
        }

        var adminUsername = _tokenService.GetUsernameFromToken(token.Replace("Bearer ", ""));
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        _loggingService.LogInformation($"User {user.Username} was deleted by {adminUsername}.");

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new LoginResponse { Token = token });
    }

    [HttpGet("getAllUsernames")]
    public async Task<IActionResult> GetAllUsernames([FromHeader(Name = "Authorization")] string token)
    {
        if (!_tokenService.IsAdmin(token.Replace("Bearer ", "")))
        {
            return Unauthorized("Only admins can view all usernames.");
        }

        var usernames = await _context.Users.Select(u => u.Username).ToListAsync();
        return Ok(new UsersGetAllUsernamesResponse { Usernames = usernames });
    }
}
