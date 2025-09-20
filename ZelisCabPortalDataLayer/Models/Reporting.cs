using System;
using System.Collections.Generic;
namespace ZelisCabPortalDataLayer.Models
{

    public  class Reporting
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ManagerId { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }


    }
}