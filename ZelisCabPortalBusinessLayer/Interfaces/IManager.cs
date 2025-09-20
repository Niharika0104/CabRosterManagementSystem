using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Interfaces
{
    public interface IManager
    {
        public Task<Boolean> ProcessRequest(ProcessDTO request);
        public Task<List<Employee>> PendingApprovals(int employeeId);
       // public Task<List<Employee>> GetAllApprovals(Employee employee);
        public Task<List<Employee>> GetAllEmployees(int id);
    }
}
