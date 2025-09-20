using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZelisCabPortalCoreLayer.Models
{
    public class RosterRequest
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public string? FullName { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime RejectedDate { get; set; }
        public string? Comment { get; set; }
        public int Status { get; set; }
    }
}
