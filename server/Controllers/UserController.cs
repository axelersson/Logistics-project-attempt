using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Data; // Ensure this namespace contains YourDbContext
using Microsoft.Extensions.Logging; // Import for logging

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly YourDbContext _context; // Replace with the actual name of your DbContext
    private readonly ILogger<UsersController> _logger; // Logger instance

    // Inject logger into the constructor
    public UsersController(YourDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _logger.LogInformation("POST request received to create user: {Username}", user.Username); // Log the POST request
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }
}
