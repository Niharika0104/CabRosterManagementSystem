using System;
using System.Collections.Generic;

namespace ZelisCabPortalDataLayer.Models
{

    public  class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; }
      
        public string PhoneNumber { get; set; } = null!;

        public int ActiveStatus { get; set; }

       

        public int CabService { get; set; }


    }
}
