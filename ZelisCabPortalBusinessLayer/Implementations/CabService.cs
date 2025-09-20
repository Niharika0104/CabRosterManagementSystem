using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
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
    public class CabService : ICabService
    {
        private DatabaseContext _db;
        public CabService(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<Boolean> AvailService(int employeeId)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    int rowsAffected = await connection.ExecuteAsync("update Employees set CabService = 1 where employeeid = @employeeid", new { employeeid = employeeId });
                    return true;
                }
                }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return false;
        }

        public async Task<RosterRequest> createRoster(RosterRequest request)
        {
            try
            {
                var query = @"INSERT INTO CabRosterRequestData (EmployeeId, StartDate, EndDate,AppliedData, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate,Status)
                     OUTPUT inserted.*
                      VALUES (@employeeid, @startdate, @enddate,@applieddate, @createdby, @createddate, @modifiedby, @modifieddate,@status)
                      "; 

                var parameters = new
                {
                    employeeid = request.EmployeeId,
                    startdate = request.StartDate,
                    enddate = request.StartDate.AddDays(5),
                    applieddate=DateTime.Now.Date,
                    status =request.Status,
                    createdby = "Niharika",
                    createddate = DateTime.Now.Date,
                    modifiedby = "Niharika",
                    modifieddate = DateTime.Now.Date
                };

                using (var connection =  _db.CreateConnection())
                {
                    // Execute the query and retrieve the inserted record
                    var insertedRecord = await connection.QueryFirstOrDefaultAsync<RosterRequest>(query, parameters);
                    if(insertedRecord!=null)
                    return insertedRecord;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<Boolean> TerminateService(int employeeid)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    int rowsAffected = await connection.ExecuteAsync("update Employees set CabService = 5 where EmployeeId = @employeeid", new { employeeid = employeeid });
                    if (rowsAffected == 1) return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return false;
        }

        public async Task<List<RosterRequest>> GetAllRosters(int employeeid)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    string sqlQuery = "SELECT Id, EmployeeId, StartDate, EndDate,  " +
    " Status, AppliedData AS AppliedDate, ApprovedDate, " +
    "RejectedDate, Comment FROM CabRosterRequestData WHERE EmployeeId = @employeeid;";

                    var response = await connection.QueryAsync<RosterRequest>(sqlQuery,new { employeeid = @employeeid });
   List<RosterRequest> list = response.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return new List<RosterRequest>();
        }

        public async Task<List<CabData>> GetAllCabs()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    string query = "SELECT CabsData.Id,CabsData.VehicleNumber, DriversData.Name AS Name, DriversData.PhoneNumber AS PhoneNumber, CabsData.TotalSeats, CabsData.AvailableSeats ,IsAvailable " +
                "FROM dbo.CabsData " +
                "JOIN dbo.DriversData ON CabsData.DriverId = DriversData.Id;";

                    // Execute the query and return the result
                    var response =     await connection.QueryAsync<CabData>(query);
                   List<CabData>list = response.ToList();
                    return list;
                }
            }
            catch(Exception ex)
            {

            }
            return new List<CabData>();

        }

        public async Task<List<DriverData>> GetAllDrivers()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    string query = "SELECT * FROM DriversData";
                    var response = await connection.QueryAsync<DriverData>(query);
                    List<DriverData> list = response.ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<DriverData>();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {
                    var response = await connection.QueryAsync<Employee>("select * from Employees where CabService=@status", new { status = 2 });
                    List<Employee> list = response.ToList();
                    return list;
                }
            }
            catch (Exception) { }
            return new List<Employee>();
        }
        public async Task<int> GetRequestCount(DateTime startDate, int employeeId)
        {
            using (var connection = _db.CreateConnection())
            {
                try
                {

                    // Use Dapper to execute the query with parameters
                    var query = "SELECT COUNT(*) FROM CabRosterRequestData WHERE StartDate = @StartDate AND EmployeeId = @EmployeeId and (Status=1 or Status=2)";
                    var parameters = new { StartDate = startDate, EmployeeId = employeeId };

                    int result = await connection.QuerySingleAsync<int>(query, parameters);

                    return result;
                }catch(Exception ex)
                {
                    throw;
                }
            }
        }


    }
}
