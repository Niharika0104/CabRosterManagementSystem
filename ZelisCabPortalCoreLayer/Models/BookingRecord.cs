using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class BookingRecord
    {
        public DateTime BookingDate { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }
        public TimeSpan PickUpTime { get; set; }
        public string VehicleNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
       
        public int StatusId { get; set; }
    }
}
