using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly FirebaseService _firebaseService;
    private readonly ILogger<UserController> _logger;

    // A static list to hold users data temporarily
    // In a real application, you would query this from a database
    private static List<User> users = new List<User>();

    public UserController(FirebaseService firebaseService, ILogger<UserController> logger)
    {
        _firebaseService = firebaseService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        _logger.LogInformation("GetAll endpoint called");
        return Ok(users); // Return the list of users
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(string id)
    {
        _logger.LogInformation($"GetById endpoint called with id: {id}");

        var user = users.Find(u => u.UserId == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
public async Task<IActionResult> AddUser([FromBody] User newUser)
{
    _logger.LogInformation("AddUser endpoint called");

    // Check if user already exists
    var existingUser = users.Find(u => u.Username == newUser.Username);
    if (existingUser != null)
    {
        return BadRequest("User already exists.");
    }

    // Hash the password using BCrypt
    newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

    // Path where the user data will be stored. You can customize the path as per your requirement.
    string path = $"users/{newUser.UserId}";

    // Adding user to the local list (simulating database save)
    users.Add(newUser);

    // Sending POST request to Firebase
    var response = await _firebaseService.PostDataAsync(path, newUser);

    if (response.IsSuccessStatusCode)
    {
        return Ok(new { message = "User added successfully", userId = newUser.UserId });
    }

    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
}

}
