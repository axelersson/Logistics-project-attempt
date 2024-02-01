using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // For JObject



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
     // Corrected the placement of DeserializeUsers method
    private List<User> DeserializeUsers(string usersData)
{
    var usersList = new List<User>();
    var usersJObject = JObject.Parse(usersData);

    foreach (var userProperty in usersJObject.Properties())
    {
        var userId = userProperty.Name;
        var userEntries = userProperty.Value;

        foreach (var entry in userEntries.Children<JProperty>())
        {
            var userEntry = entry.Value;
            var user = userEntry.ToObject<User>();

            if (user != null)
            {
                // Ensure UserID is set correctly (it might be different from Firebase's random key)
                user.UserId = userId;
                usersList.Add(user);
            }
        }
    }

    return usersList;
}




[HttpGet] // GET ALL USERS *************************** FIX TO NOT INCLUDE PASSWORDS
public async Task<ActionResult<IEnumerable<User>>> GetAll()
{
    _logger.LogInformation("GetAll endpoint called");

    // Fetch the users from Firebase
    var response = await _firebaseService.GetDataAsync("users");

    if (!response.IsSuccessStatusCode)
    {
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    var usersData = await response.Content.ReadAsStringAsync();
    _logger.LogInformation(usersData);
    // Deserialize the data from Firebase into User objects
    // Assuming _firebaseService.GetDataAsync returns the JSON structure directly from Firebase
    var users = DeserializeUsers(usersData);

    return Ok(users);
}
[HttpGet("{id}")] // GET BY ID ***************************************************************** THIS RETURNS PASSWORDS
public async Task<ActionResult<User>> GetById(string id)
{
    _logger.LogInformation($"GetById endpoint called with id: {id}");

    // Fetch the user from Firebase
    var response = await _firebaseService.GetDataAsync($"users/{id}");
    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        return StatusCode((int)response.StatusCode, responseContent);
    }

    // Deserialize the response content into a dictionary and then get the first user
    var usersDict = JsonConvert.DeserializeObject<Dictionary<string, User>>(responseContent);
    if (usersDict == null || usersDict.Count == 0)
    {
        return NotFound();
    }

    // Assuming you want the first user in the dictionary
    var user = usersDict.Values.FirstOrDefault();
    if (user == null)
    {
        return NotFound();
    }

    // Set UserId because Firebase's unique key is not the UserId
    user.UserId = id;

    return Ok(user);
}


    [HttpPost] // POST USER
    public async Task<IActionResult> AddUser([FromBody] User newUser)
    {
        _logger.LogInformation("AddUser endpoint called");

        // Fetch the users from Firebase to check for duplicates
        var getAllResponse = await _firebaseService.GetDataAsync("users");
        if (!getAllResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)getAllResponse.StatusCode, await getAllResponse.Content.ReadAsStringAsync());
        }

        var allUsersData = await getAllResponse.Content.ReadAsStringAsync();
        var allUsers = DeserializeUsers(allUsersData);

        var existingUser = allUsers.Find(u => u.Username == newUser.Username);
        if (existingUser != null)
        {
            return BadRequest("User already exists.");
        }

    // Hash the password using BCrypt
    newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);

    // Path where the user data will be stored. You can customize the path as per your requirement.
    string path = $"users/{newUser.UserId}";

    // Sending POST request to Firebase
    var response = await _firebaseService.PostDataAsync(path, newUser);

    if (response.IsSuccessStatusCode)
    {
        return Ok(new { message = "User added successfully", userId = newUser.UserId });
    }

    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
}


[HttpPost("login")] // LOGIN USER
public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
{
    _logger.LogInformation("Login endpoint called");

    // Fetch all users from Firebase
    var response = await _firebaseService.GetDataAsync("users");
    if (!response.IsSuccessStatusCode)
    {
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    var usersData = await response.Content.ReadAsStringAsync();
    var users = DeserializeUsers(usersData);

    var user = users.SingleOrDefault(u => u.Username == loginRequest.Username);
    if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
    {
        return Unauthorized("Invalid username or password.");
    }

    // Here, you should generate a token or session identifier for the logged-in user
    // For the purpose of this example, we'll just return a success message

    return Ok(new { message = "Login successful" });
}


public class LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}


[HttpPut("{id}")] // UPDATE USER
public async Task<IActionResult> UpdateUser(string id, [FromBody] User updatedUser)
{
    _logger.LogInformation($"UpdateUser endpoint called with id: {id}");

    // Check if the user exists in Firebase
    var fetchResponse = await _firebaseService.GetDataAsync($"users/{id}");
    if (!fetchResponse.IsSuccessStatusCode)
    {
        // If user doesn't exist or another error occurred, reflect that in the response
        return StatusCode((int)fetchResponse.StatusCode, await fetchResponse.Content.ReadAsStringAsync());
    }

    // If the user wants to update the password, re-hash the new password
    if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
    {
        updatedUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
    }

    // Update the user in Firebase
    string path = $"users/{id}";
    var response = await _firebaseService.PutDataAsync(path, updatedUser); // Use PUT for updates

    if (!response.IsSuccessStatusCode)
    {
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    return Ok(new { message = "User updated successfully" });
}


[HttpDelete("{id}")] // DELETE USER
public async Task<IActionResult> DeleteUser(string id)
{
    _logger.LogInformation($"DeleteUser endpoint called with id: {id}");

    // Attempt to delete the user in Firebase directly
    string path = $"users/{id}";
    _logger.LogInformation($"Attempting to delete user at path: {path}");

    var response = await _firebaseService.DeleteDataAsync(path);

    // If Firebase responds that the user was not found, reflect that in the response
    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return NotFound();
    }

    // Check for any other types of unsuccessful responses
    if (!response.IsSuccessStatusCode)
    {
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    // If the delete was successful, return an appropriate response
    return Ok(new { message = "User deleted successfully" });
}


}
