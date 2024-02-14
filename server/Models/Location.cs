using System.Text.Json.Serialization;

public class Location
{
    public string LocationId { get; set; } = Guid.NewGuid().ToString();
    public string AreaId { get; set; } = string.Empty;
  
    public enum LocationType
    {
        Storage,
        Machine
    }

    // Navigation Properties
      public Area? Area { get; set; }
      public List<RollOfSteel> RollsOfSteel { get; set; } = new List<RollOfSteel>();
      public List<Order> Orders { get; set; } = new List<Order>();
}
