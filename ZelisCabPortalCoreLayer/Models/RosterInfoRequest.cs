



namespace ZelisCabPortalCoreLayer.Models
{
   
    public class RosterInfoRequest
    {
        
        public string pickup { get; set; }
        public string drop { get; set; }
        public int shift { get; set; }
        public int employeeid { get; set; }
        public DateTime dateofbooking { get; set; }

        public TimeSpan pickupTime { get; set; }
        public DateTime droptime { get; set; } = DateTime.Now.AddMinutes(90);
        public int StatusId { get; set; }




        
    }
}
