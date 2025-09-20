using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class CabBookingRosterInfo
    {
      
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public int CabId { get; set; }
            public DateTime BookingDate { get; set; }
            public int DriverId { get; set; }
            public string PickUpLocation { get; set; }
            public string DropLocation { get; set; }
            public TimeSpan PickUpTime { get; set; }
            public int StatusId { get; set; }
          public int Shift { get; set; }
            public int RosterId { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string VehicleNumber { get; set; }
       

    }
}
