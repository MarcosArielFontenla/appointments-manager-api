namespace Appointments.Data.Entities
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
