public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string UserID { get; set; } = string.Empty;

    public enum OrderStatus 
    {
        Pending,
        PartiallyDelivered,
        Delivered,
        Cancelled
    }
    public string SourceId { get; set; } = string.Empty; 
    public string DestinationId { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = new DateTime();

    // Navigation Properties
    public User? User { get; set; }
    public Location? SourceLocation { get; }
    public Location? DestinationLocation { get; }
    public List<OrderRoll> OrderRolls { get; set; } = new List<OrderRoll>();
    public List<TruckOrderAssignment> TruckOrderAssignments { get; set; } = new List<TruckOrderAssignment>();
}
