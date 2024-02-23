using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class UserIdAndRoleResponseFromUsernameRequest
{
    public string UserId { get; set; }
    public string Username { get; set; } // Add this line to include the Username in the model
    
    [JsonConverter(typeof(StringEnumConverter))] // Serialize the enum as a string
    public User.UserRole Role { get; set; }

    // Update constructor to include the Username parameter
    public UserIdAndRoleResponseFromUsernameRequest(string userId, string username, User.UserRole role)
    {
        UserId = userId;
        Username = username; // Assign the Username
        Role = role;
    }
}
