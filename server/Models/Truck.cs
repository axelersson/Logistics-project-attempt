public class Truck
{
    public string TruckId { get; set; } = Guid.NewGuid().ToString();
    public string CurrentAreaId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
    public Area? CurrentArea { get; set; }
}
