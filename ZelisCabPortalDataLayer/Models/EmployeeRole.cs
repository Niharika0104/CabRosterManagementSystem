using System;
using System.Collections.Generic;
namespace ZelisCabPortalDataLayer.Models
{

    public  class EmployeeRole
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int RoleId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


    }
}
