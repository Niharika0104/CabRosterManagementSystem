using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZelisCabPlatform.Validations;


namespace ZelisCabPlatform.Models
{
    [ComplexType]
    public class RosterInfo
    {
        public RosterInfo(string pickup, string drop, int shift, int employeeid)
        {
            this.pickup = pickup;
            this.drop = drop;
            this.shift = shift;
            this.employeeid = employeeid;
        }
        public RosterInfo() { }



        public DateTime PickupDateTime
        {
            get => DateTime.Today.Add(pickupTime);
            set => pickupTime = value.TimeOfDay;
        }

        [PickupDropValidator(ErrorMessage = "This field is required for shifts 1, 2, and 3.")]
        public string pickup { get; set; }
        [PickupDropValidator(ErrorMessage = "This field is required for shifts 1, 2, and 3.")]
        public string drop { get; set; }
        [Range(1, 5, ErrorMessage = "This is a required filed.")]
        public int shift { get; set; }
        [Required]
        public int employeeid { get; set; }
        public DateTime dateofbooking { get; set; }

        public TimeSpan pickupTime { get; set; }
        public DateTime droptime { get; set; } = DateTime.Now.AddMinutes(90);
        public int StatusId { get; set; }
    }
}
