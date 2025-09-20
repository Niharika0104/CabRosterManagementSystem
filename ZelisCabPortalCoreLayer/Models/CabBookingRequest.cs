using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class CabBookingRequest
    {
        

        public int EmployeeId { get; set; }

       

        public DateTime BookingDate { get; set; }

       

        public string PickUpLocation { get; set; } = null!;

        public string DropLocation { get; set; } = null!;

        public DateTime pickuptime { get; set; }
        public DateTime droptime { get; set; } = DateTime.Now.AddMinutes(90);
        public int StatusId { get; set; }
        public DateTime droplocation { get; set; }



       
    }
}
