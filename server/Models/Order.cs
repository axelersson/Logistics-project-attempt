
using System.Text.Json.Serialization;

public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string UserID { get; set; } = string.Empty;

    public OrderStatus OrderStatus { get; set; }
    public string DestinationId { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = new DateTime();

    // Navigation Properties
    [JsonIgnore]
    public User? User { get; set; }
    [JsonIgnore]
    public Location? DestinationLocation { get; set; }
    public List<OrderRoll> OrderRolls { get; set; } = new List<OrderRoll>();
    public List<TruckOrderAssignment> TruckOrderAssignments { get; set; } = new List<TruckOrderAssignment>();
}

public enum OrderStatus 
{
    Pending,
    PartiallyDelivered,
    Delivered,
    Cancelled
}
