public class Order
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = string.Empty; // Default to empty if type is mandatory, or make it nullable
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public List<string> AssociatedRollsIds { get; set; } = new List<string>();
    public string SourceMachineId { get; set; } = string.Empty;
    public string DestinationMachineId { get; set; } = string.Empty;
    public ICollection<OrderRollOfSteel> OrderRollsOfSteel { get; set; } = new List<OrderRollOfSteel>();

}
