using System;

public class UserCreateModel
{
    public UserCreateModel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public User.UserRole Role { get; set; } = User.UserRole.User;  // Default role is UserRole.User
}
