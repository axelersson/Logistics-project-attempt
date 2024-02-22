
using System.Text.Json.Serialization;

public class TruckOrderAssignment
{
    public int TruckOrderAssignmentId{ get; set; }
    public string TruckId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;

    public bool IsAssigned { get; set; } = true;

    public DateTime? AssignedAt { get; set; } = new DateTime();

    // !! Set this new DateTime when the truck is unassigned
    public DateTime? UnassignedAt { get; set; }

    
    // Navigation Properties
    [JsonIgnore]
    public Truck? Truck { get; set; }
    [JsonIgnore]
    public Order? Order { get; set; }

}
