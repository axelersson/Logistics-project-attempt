public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();

    // Remove?
    //public string Type { get; set; } = string.Empty; // Default to empty if type is mandatory, or make it nullable
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public string SourceId { get; set; } = string.Empty; // Default to empty if sourceLocation is mandatory, or make it nullable
    public string DestinationId { get; set; } = string.Empty; // Default to empty if destinationLocation is mandatory, or make it nullable
    //public Location? SourceLocation { get; set; }
    //public Location? DestinationLocation { get; set; }
    public List<RollOfSteel> RollsOfSteel { get; } = new List<RollOfSteel>();
    public List<Truck> Trucks { get; } = new List<Truck>();

    public DateTime CreatedAt { get; set; } = new DateTime();
}
