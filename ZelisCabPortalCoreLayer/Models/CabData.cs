using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class CabData
    {
        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }  
        public string PhoneNumber { get; set; }
       
        public bool IsAvailable { get; set; }
    }

}
