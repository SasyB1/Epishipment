namespace Epishipment.Models
{
   public enum ShipmentStatus
    {
        NotDelivered,
        Delivered,
        OutForDelivery,
        InTransit,
    }
    public class ShipmentState
    {
        public int ShipmentStateId { get; set; }
        public int ShipmentId { get; set; }
        public ShipmentStatus Status { get; set; }

        public string Location { get; set; }
        
        public string Description { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
