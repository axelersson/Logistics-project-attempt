public class Area
{
    public string AreaId { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty; // Default to empty if name is mandatory, or make it nullable
    public List<string> LocationIds { get; set; } = new List<string>();

    // Navigation property for the related collection of Trucks
    public ICollection<Truck> Trucks { get; set; } = new List<Truck>();

    // Navigation property for the related collection of Locations
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
