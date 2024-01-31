public class Location
{
    public string LocationId { get; set; } = Guid.NewGuid().ToString();
    public string AreaId { get; set; } = string.Empty;
    public int Capacity { get; set; } = 0; // Default to 0 or an appropriate default value
    public int CurrentOccupancy { get; set; } = 0; // Default to 0 or an appropriate default value
    public Area? Area { get; set; }
}
