public class OrderRollOfSteel
{
    public string? OrderId { get; set; }
    // Nullable navigation property for the related Order
    public Order? Order { get; set; }
    public string? RollOfSteelId { get; set; }
    // Nullable navigation property for the related RollOfSteel
    public RollOfSteel? RollOfSteel { get; set; }
}
