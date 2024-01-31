public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public enum UserRole
    {
        Admin,
        User
    }

    public UserRole Role { get; set; } = UserRole.User; // Default to User

    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Store only hash for security reasons
}
