public class RollOfSteel
{
    public string RollOfSteelId { get; set; } = Guid.NewGuid().ToString();
    public string CurrentLocationId { get; set; } = string.Empty;

    public enum RollStatus
    {
        Processed,
        Raw
    }

    // Navigation Properties
    public Location? CurrentLocation { get; set; }
    public List<OrderRoll> OrderRolls { get; set; } = new List<OrderRoll>();

}
