public class RollOfSteel
{
    public string RollOfSteelId { get; set; } = Guid.NewGuid().ToString();
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public string CurrentLocationId { get; set; } = string.Empty;
    public string DestinationLocationId { get; set; } = string.Empty;

    public ICollection<OrderRollOfSteel> OrderRollsOfSteel { get; set; } = new List<OrderRollOfSteel>();
}
