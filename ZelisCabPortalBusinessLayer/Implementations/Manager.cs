using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Implementations
{
    public class Manager : IManager
    {
        private DatabaseContext _db;
        public Manager(DatabaseContext db)
        {
            _db = db;
        }
        public async  Task<List<Employee>> GetAllEmployees(int id)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    var response = await connection.QueryAsync<Employee>("select * from Employees join ReportingData on Employees.EmployeeId=ReportingData.EmployeeId and ReportingData.ManagerId=@id", new { id = id });
                    List<Employee> list = response.ToList();
                    return list;
                }
            }
            catch (Exception) { }
            return new List<Employee>();
        }

       

        public async Task<List<Employee>> PendingApprovals(int employeeId)
        {
            using (var connection = _db.CreateConnection())
            {
                var response = await connection.QueryAsync<Employee>("select * from Employees join ReportingData on Employees.EmployeeId=ReportingData.EmployeeId and ReportingData.ManagerId=@id where Employees.CabService=1", new { id = employeeId });
                List<Employee> list = response.ToList();
                return list;
            }
        }

        public async Task<Boolean> ProcessRequest(ProcessDTO request)
        {
            try
            {
                //update employees set cabservice = 1 where employeeid = @employeeid", new { employeeid = employee.EmployeeId }
                using (var connection = _db.CreateConnection())
                {
                    var response = await connection.ExecuteAsync("update Employees set CabService = @type where employeeid = @employeeid", new { employeeid = request.EmployeeId, type = request.Status });
                    if (response==1) return true;

                }
            }
            catch (Exception) { }
            return false;
        }
    }
}
