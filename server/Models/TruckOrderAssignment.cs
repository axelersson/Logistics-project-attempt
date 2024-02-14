
using System.Text.Json.Serialization;

public class TruckOrderAssignment
{
    public int TruckOrderAssignmentId{ get; set; }
    public string TruckId { get; set; } = string.Empty;
    
    public string OrderId { get; set; } = string.Empty;
    
    // Navigation Properties
    [JsonIgnore]
    public Truck? Truck { get; set; }
    [JsonIgnore]
    public Order? Order { get; set; }

    public DateTime? AssignmentAt { get; set; }
    public DateTime? UnassignmentAt { get; set; }

}
