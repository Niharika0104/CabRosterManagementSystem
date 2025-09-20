using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class ProcessBookRequest
    {
        public CabBookingRequest bookrequest { get; set; }
     
        public int cabId { get; set; }
       
    }
}
