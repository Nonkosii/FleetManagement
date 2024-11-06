namespace FleetManagement.Model
{
    public class VehicleLocation
    {
        public int Id { get; set; }
        public string? VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
