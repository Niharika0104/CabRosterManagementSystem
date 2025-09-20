using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Interfaces
{
    public interface ICabService
    {

        public Task<Boolean> AvailService(int employeeid);
        public Task<Boolean> TerminateService(int employeeid);
        public Task<RosterRequest> createRoster(RosterRequest roster);
        public Task<List<RosterRequest>> GetAllRosters(int employeeid);
        public Task<List<CabData>> GetAllCabs();
        public Task<List<DriverData>> GetAllDrivers();
        public Task<List<Employee>> GetAllEmployees();
        public Task<int> GetRequestCount(DateTime startDate, int employeeId);


    }
}
