
using System.Text.Json.Serialization;

public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string? UserID { get; set; } = string.Empty;

    // Set OrderStatus to pending by default
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public string? ToLocId { get; set; } = string.Empty; 
    public string? FromLocId { get; set; } = string.Empty;
    public int Pieces { get; set; } = 0;
    public OrderType? OrderType { get; set; }
    public DateTime CreatedAt { get; set; } = new DateTime();
    public DateTime? CompletedAt { get; set; }



    // Navigation Properties
    [JsonIgnore]
    public User? User { get; set; }
    [JsonIgnore]
    public Location? ToLocation { get; set; }
    [JsonIgnore]
    public Location? FromLocation { get; set; }
    public List<TruckOrderAssignment> TruckOrderAssignments { get; set; } = new List<TruckOrderAssignment>();
}

public enum OrderStatus 
{
    Pending,
    PartiallyDelivered,
    Delivered,
    Cancelled
}

public enum OrderType
{
    Recieving,
    Sending
}
