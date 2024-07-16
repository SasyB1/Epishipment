﻿namespace Epishipment.Models
{
    public class ShipmentState
    {
        public int ShipmentStateId { get; set; }
        public int ShipmentId { get; set; }
        public string Status { get; set; }

        public string Location { get; set; }
        
        public string Description { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}