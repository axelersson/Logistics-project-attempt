using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
public class Truck
    {
        public string TruckId { get; set; } = Guid.NewGuid().ToString();
        public string CurrentAreaId { get; set; } = string.Empty;

    public string registrationnumber { get; set; } = string.Empty;

    // Navigation Properties
    [JsonIgnore]
    public Area? CurrentArea { get; set; }
    public List<TruckUser> TruckUsers { get; set; } = new List<TruckUser>();
    public List<TruckOrderAssignment> TruckOrderAssignments { get; set; } = new List<TruckOrderAssignment>();
}
