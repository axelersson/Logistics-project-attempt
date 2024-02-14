
public class TruckUser
{
    public int TruckUserId { get; set; }

    public string? TruckId { get; set; }
    public Truck? Truck { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public bool IsAssigned { get; set; } = true;
    public DateTime? DateAssigned { get; set; }
    public DateTime? DateUnassigned { get; set; }



}
