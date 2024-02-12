public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty; // Default to empty if type is mandatory, or make it nullable
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public Location? SourceLocation { get; set; }
    public Location? DestinationLocation { get; set; }
    
    // Add this line to define the relationship
    public ICollection<OrderRollOfSteel> OrderRollsOfSteel { get; set; } = new List<OrderRollOfSteel>();
}
