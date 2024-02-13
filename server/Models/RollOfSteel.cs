public class RollOfSteel
{
    public string RollOfSteelId { get; set; } = Guid.NewGuid().ToString();

    //Current location of the roll of steel
    public string CurrentLocationId { get; set; } = string.Empty;
    public Location? CurrentLocation { get; set; }
    //public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable

    public List<Order> Orders { get; } = new List<Order>();
}
