using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class AdminProcessBookingRequest
    {
       public RosterRequest Request { get; set; }
       public CabData CabInfo { get; set; }
    }
}
