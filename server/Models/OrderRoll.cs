public class OrderRoll
{
    public int OrderRollId { get; set; }
    public string? OrderId { get; set; }
    public string? RollOfSteelId { get; set; }
    public enum OrderRollStatus
    {
        Pending,
        Delivered,
        Cancelled
    }

    // Navigaion properties

    public Order? Order { get; set; }
    public RollOfSteel? RollOfSteel { get; set; }
}
