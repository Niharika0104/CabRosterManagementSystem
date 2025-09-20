using System;
using System.Collections.Generic;

namespace ZelisCabPortalDataLayer.Models
{

    public class Cabs
    {
        public int Id { get; set; }

        public string VehicleNumber { get; set; } = null!;

        public int DriverId { get; set; }

        public int TotalSeats { get; set; }

        public int AvailableSeats { get; set; }
        public int IsAvailable { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


    }
}