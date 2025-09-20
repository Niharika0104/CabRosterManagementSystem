using System;
using System.Collections.Generic;

namespace ZelisCabPortalDataLayer.Models
{

    public class CabBookings
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int? CabId { get; set; }

        public DateTime BookingDate { get; set; }

        public int? DriverId { get; set; }

        public string PickUpLocation { get; set; } = null!;

        public string DropLocation { get; set; } = null!;

        public TimeSpan PickUpTime { get; set; }

        public int StatusId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


    }
}