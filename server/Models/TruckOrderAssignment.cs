
public class TruckOrderAssignment
{
    public string RowId { get; set; } = Guid.NewGuid().ToString();
    public string TruckId { get; set; } = string.Empty;
    public Truck? Truck { get; set; }

    public string OrderId { get; set; } = string.Empty;
    public Order? Order { get; set; }

    public DateTime? AssignmentAt { get; set; }
    public DateTime? UnassignmentAt { get; set; }

}
