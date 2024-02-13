using System.Text.Json.Serialization;

public class Location
{
    public string LocationId { get; set; } = Guid.NewGuid().ToString();
    public string AreaId { get; set; } = string.Empty;


    //public ICollection<RollOfSteel> RollsOfSteel { get; set; } = new List<RollOfSteel>();
    //public RollOfSteel? RollOfSteel { get; set; }
    public Area? Area { get; set; }
}
