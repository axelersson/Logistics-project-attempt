public class Truck
{
    public string TruckId { get; set; } = Guid.NewGuid().ToString();
    public string CurrentAreaId { get; set; } = string.Empty;
    public Area? CurrentArea { get; set; }
    public List<TruckUser> TruckUsers { get; set; } = new List<TruckUser>();
    public List<TruckOrderAssignment> TruckOrderAssignments { get; set; } = new List<TruckOrderAssignment>();
}
