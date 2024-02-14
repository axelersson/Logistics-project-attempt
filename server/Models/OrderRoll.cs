using System.Text.Json.Serialization;

public class OrderRoll
{
    public int OrderRollId { get; set; }
    public string? OrderId { get; set; }
    public string? RollOfSteelId { get; set; }
    public OrderRollStatus OrderRollStatus { get; set; }

    // Navigaion properties

    [JsonIgnore]
    public Order? Order { get; set; }
    public RollOfSteel? RollOfSteel { get; set; }
}

public enum OrderRollStatus
{
    Pending,
    Delivered,
    Cancelled
}