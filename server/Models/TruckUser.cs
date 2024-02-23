
using System.Text.Json.Serialization;

public class TruckUser
{
    public int TruckUserId { get; set; }

    public string? TruckId { get; set; }
    
    public string? UserId { get; set; }

    // IsAssigned is a property for keeping track if the truck is currently assigned to a user
    // Should be set to false when the truck is unassigned and the row should be kept for monitoring reasons,
    // to keep track of when an truck was assigned to an area by using the AssignmentAt and UnassignmentAt properties
    public bool IsAssigned { get; set; } = true;
    public DateTime AssignedAt { get; set; } = new DateTime();
    public DateTime? UnassignedAt { get; set; }

    // Navigation Properties
    [JsonIgnore]
    public Truck? Truck { get; set; }

    [JsonIgnore]
    public User? User { get; set; }



}
