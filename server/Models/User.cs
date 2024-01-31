public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public string Role { get; set; } = string.Empty; // Default to "User" if there's a default role, or make it nullable
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Store only hash for security reasons
}
