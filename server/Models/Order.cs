public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty; // Default to empty if type is mandatory, or make it nullable
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public Location? SourceLocation { get; set; }
    public Location? DestinationLocation { get; set; }
    public object OrderRollsOfSteel { get; internal set; }
}
