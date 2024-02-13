using System.Text.Json.Serialization;

public class Area
{
    public string AreaId { get; set; } = Guid.NewGuid().ToString();
    public List<Truck> Trucks { get; } = new List<Truck>();
    //public ICollection<Location> Locations { get; set; } = new List<Location>();
    //public ICollection<Machine> Machines { get; set; } = new List<Machine>();
    //public ICollection<Truck> Trucks { get; set; } = new List<Truck>();

}
