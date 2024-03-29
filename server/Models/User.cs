using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString();

    // Define the UserRole enum inside the User class
    [JsonConverter(typeof(StringEnumConverter))] // Ensure enums are serialized as strings
    public UserRole Role { get; set; } = UserRole.User;

    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    // !! Removed Trucks property since they are related by the TruckUser table now

    // public List<Truck> Trucks { get; } = new List<Truck>();

    // UserRole enum definition
    public enum UserRole
    {
        Admin,
        User
    }
    // Navigation Properties
    public List<TruckUser> TruckUsers { get; set; } = new List<TruckUser>();
    public List<Order> Orders { get; set; } = new List<Order>();
}
