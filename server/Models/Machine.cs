public class Machine
{
    public string MachineId { get; set; } = Guid.NewGuid().ToString();
    //public string Type { get; set; } = string.Empty; // Default to empty if type is mandatory, or make it nullable
    public string AreaID { get; set; } = string.Empty; // Default to empty if area is mandatory, or make it nullable
    //public Area? Area { get; set; }
    //public string Status { get; set; } = string.Empty; // Default to empty if status is mandatory, or make it nullable
}
