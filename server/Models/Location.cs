using System.Text.Json.Serialization;

public class Location
{
    public string LocationId { get; set; } = Guid.NewGuid().ToString();
    public string AreaId { get; set; } = string.Empty;
    public LocationType LocationType { get; set; }

    // Navigation Properties

    // OBSOLETE
    // public List<RollOfSteel> RollsOfSteel { get; set; } = new List<RollOfSteel>();
    public List<Order> FromOrders { get; set; } = new List<Order>();
    public List<Order> ToOrders { get; set; } = new List<Order>();

    [JsonIgnore]
     public Area? Area { get; set; }
}

public enum LocationType
{
    Storage,
    Machine
}
