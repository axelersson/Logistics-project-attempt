using System.Text.Json.Serialization;

public class Area
{
    public string AreaId { get; set; } = Guid.NewGuid().ToString();

    // Navigation Properties
    public List<Location> Locations { get; set; } = new List<Location>();
    public List<Truck> Trucks { get; set; } = new List<Truck>();

}
