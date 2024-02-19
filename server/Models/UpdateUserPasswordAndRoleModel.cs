using System;

public class UpdateUserPasswordAndRoleModel
{
    public string? UserId { get; set; }
    public User.UserRole Role { get; set; }
    public string? NewPassword { get; set; }
}
