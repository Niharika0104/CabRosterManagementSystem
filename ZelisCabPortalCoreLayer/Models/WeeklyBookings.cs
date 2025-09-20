using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ZelisCabPortalCoreLayer.Models
{
    public class WeeklyBookings
    {
        public WeeklyBookings()
        {
            rosterlist = new List<RosterInfoRequest>();
        }
        public List<RosterInfoRequest>? rosterlist { get; set; }
        public int RosterId { get; set; }
    }
}
