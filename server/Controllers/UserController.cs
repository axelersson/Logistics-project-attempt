using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Data; // Ensure this namespace contains YourDbContext
using Microsoft.Extensions.Logging; // Import for logging;
using BCrypt.Net; // Import for bcrypt hashing
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
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
    public IActionResult Login([FromBody] User userToLogin)
    {
    var user = _context.Users.FirstOrDefault(u => u.Username == userToLogin.Username);
    if (user == null)
    {
        return Unauthorized();
    }

    // Verify the password using BCrypt
    if (BCrypt.Net.BCrypt.Verify(userToLogin.PasswordHash, user.PasswordHash))
    {
        return Ok(user);
    }
    else
    {
        return Unauthorized();
    }
    }
}
