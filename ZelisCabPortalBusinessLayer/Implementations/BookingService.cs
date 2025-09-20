using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalCoreLayer.Models;

using ZelisCabPortalDataLayer;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly DatabaseContext _db;
        public BookingService(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<Boolean> bookACab(List<RosterInfoRequest> request, int rosterid)
        {
            try
            {
                bool valid = true;
                using (var connection = _db.CreateConnection())
                {
                    foreach (RosterInfoRequest book in request)
                    {
                        if (book.shift >= 1 && book.shift <= 3) {
                            var query = "INSERT INTO CabBookingsData  (EmployeeId, BookingDate, PickUpLocation, DropLocation, PickUpTime, StatusId, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate,RosterId)       VALUES (@EmployeeId, @BookingDate, @PickUpLocation, @DropLocation, @PickUpTime, @StatusId, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate,@RosterId)";
                            var parameters = new
                            {
                                EmployeeId = book.employeeid,

                                BookingDate = book.dateofbooking
                                ,

                                PickUpLocation = book.pickup,
                                DropLocation = book.drop,
                                PickUpTime = book.pickupTime,
                                RosterId = rosterid,

                                StatusId = 1,
                                CreatedBy = "Niharika",
                                CreatedDate = DateTime.Now,
                                ModifiedBy = "Niharika",
                                ModifiedDate = DateTime.Now
                            };

                            var response = await connection.QueryAsync<CabBookings>(query, parameters);

                            if (response == null) return false;
                        }
                    }
                }
                return valid;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<List<Cabs>> getAllAvailableCabs()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "select * from CabsData where IsAvailable=@value;";


                    var response = await connection.QueryAsync<Cabs>(query, new { value = 1 });

                    return response.ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<Cabs>();

        }

        public async Task<List<CabBookings>> getAllBookings()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "select * from CabBookingsData ;";


                    var response = await connection.QueryAsync<CabBookings>(query);

                    return response.ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<CabBookings>();

        }

        public async Task<List<Cabs>> getAllCabs()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "select * from CabsData;";


                    var response = await connection.QueryAsync<Cabs>(query);

                    return response.ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<Cabs>();
        }

        public async Task<List<Drivers>> getAllDrivers()
        {

            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "select * from DriversDetails;";


                    var response = await connection.QueryAsync<Drivers>(query);

                    return response.ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<Drivers>();
        }

        public async Task<List<Employee>> getAllEmployees()
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "select * from Employees where CabService=@value;";


                    var response = await connection.QueryAsync<Employee>(query, new { value = 2 });

                    return response.ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<Employee>();
        }
        //if getAllAvailableCabs are greater than 0 then this will be called else another method will be called
        public async Task<Boolean> ProcessBooking(ProcessBookRequest request)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    var query = "UPDATE CabBookingsData    SET   CabId = @CabId    WHERE Id=@id";


                    var response = await connection.QueryAsync<Employee>(query, new { CabId = request.cabId, id = request.bookrequest.EmployeeId });

                    if (response != null) return true;

                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        public async Task<List<BookingRecord>> getCurrentWeekBookings(int employeeid)
        {
            try
            {
                DateTime mondaydate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

                using (var connection = _db.CreateConnection())
                {

                    var result = await connection.QueryAsync<BookingRecord>(
                                    @"SELECT  EmployeeId, CabId,CabsData.VehicleNumber ,BookingDate, CabsData.DriverId,DriversData.Name  ,DriversData.PhoneNumber ,PickUpLocation, DropLocation, PickUpTime, StatusId, RosterId
FROM (
    SELECT CabBookingsData.EmployeeId, CabId, BookingDate,  PickUpLocation, DropLocation, PickUpTime, StatusId, RosterId
    FROM CabBookingsData
        INNER JOIN CabRosterRequestData ON CabBookingsData.RosterId = CabRosterRequestData.Id
    WHERE CabRosterRequestData.Status = 2 and CabRosterRequestData.EmployeeId=@EmployeeId
 AND CabBookingsData.BookingDate >= @MondayDate
                AND CabBookingsData.BookingDate <= CabRosterRequestData.EndDate
) AS E
INNER JOIN CabsData ON E.CabId = CabsData.Id
INNER JOIN DriversData ON DriversData.Id = CabsData.DriverId;
",
                                    new { EmployeeId = employeeid ,MondayDate=mondaydate}
                                );

                    List<BookingRecord> list = result.ToList();


                    if (result != null) return list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new List<BookingRecord>();
        }
        public async Task<List<RosterRequest>> getpendingrosters()
        {
            try
            {

                using (var connection = _db.CreateConnection())
                {

                    var result = await connection.QueryAsync<RosterRequest>(
                                   "select CabRosterRequestData.Id,CabRosterRequestData.EmployeeId,StartDate,EndDate,CabRosterRequestData.AppliedData as AppliedDate,(FirstName+' '+LastName) as FullName,Status,Comment from CabRosterRequestData join Employees on CabRosterRequestData.EmployeeId=Employees.EmployeeId where CabRosterRequestData.Status=1;");




                    List<RosterRequest> list = result.ToList();


                    if (result != null) return list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<Boolean> ApproveRosterRequest(RosterRequest request,CabData cabdata)
        {
            IDbTransaction transaction=null;
            try
            {
                using (var connection = await _db.CreateConnectionAsync())
                {

                  
                    using (transaction =  connection.BeginTransaction())
                    {

                        string updateQuery = @"UPDATE CabRosterRequestData 
                                       SET ApprovedDate = @ApprovedDate,Status=2
                                       WHERE Id = @Id";


                        await connection.ExecuteAsync(updateQuery, new { ApprovedDate = DateTime.Now.Date, Id = request.Id }, transaction);

                        string updatebookings = @"UPDATE CabBookingsData 
                                       SET StatusId = 1,CabId=@CabId

                                       WHERE   RosterId=@RosterId and EmployeeId=@EmployeeId";
                        await connection.ExecuteAsync(updatebookings, new {  EmployeeId = request.EmployeeId, CabId = cabdata.Id,RosterId=request.Id }, transaction);

                        transaction.Commit();


                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and rollback the transaction
                Console.WriteLine("Error updating ApprovedDate: " + ex.Message);
                transaction?.Rollback();

                // Return false to indicate failure
                return false;
            }
                
            
        }
        public async Task<Boolean> RejectRosterRequest(RosterRequest request)
        {
            using (var connection = await _db.CreateConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        string updateQuery = @"UPDATE CabRosterRequestData 
                                       SET RejectedDate = @RejectedDate,Comment=@Comment,Status=3
                                       WHERE Id = @Id";


                        await connection.ExecuteAsync(updateQuery, new { RejectedDate = DateTime.Now.Date, Id = request.Id ,Comment=request.Comment}, transaction);

                        string updatebookings = @"UPDATE CabBookingsData 
                                       SET StatusId = 3

                                       WHERE Id = @Id and RosterId=@RosterId and EmployeeId=@EmployeeId";
                        await connection.ExecuteAsync(updatebookings, new { Id = request.Id, RosterId = request.Id,EmployeeId=request.EmployeeId}, transaction);

                        transaction.Commit();


                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions and rollback the transaction
                        Console.WriteLine("Error updating ApprovedDate: " + ex.Message);
                        transaction.Rollback();

                        // Return false to indicate failure
                        return false;
                    }
                }
            }
        }
        public async  Task<List<CurrentDayBooking>> DateWiseBookings(DateTime date,string vehicleno)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    string sqlQuery = @"
      SELECT 
      (Employees.FirstName + ' ' + Employees.LastName) AS EmployeeName,
      E.Name AS DriverName,
      E.PhoneNumber AS DriverPhoneNumber,
      PickUpLocation,
      DropLocation,
      Employees.PhoneNumber AS EmployeePhoneNumber
  FROM 
      CabBookingsData
      JOIN Employees ON Employees.EmployeeId = CabBookingsData.EmployeeId 
      join
      (select VehicleNumber,DriversData.Name,CabsData.Id,DriversData.PhoneNumber from CabsData
	  JOIN DriversData ON DriversData.Id = CabsData.DriverId) as E 
	  on E.Id=CabBookingsData.CabId
  WHERE 
      CabBookingsData.BookingDate = @date and E.VehicleNumber=@vehicleno;
";



                   var response = await connection.QueryAsync<CurrentDayBooking>(sqlQuery, new {date=date,vehicleno=vehicleno});

                    return response.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        public async Task<List<CabBookingRosterInfo>> getRosterBookingsById(int id)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {


                    string sqlQuery = @"
    select * from CabBookingsData 

join (select CabsData.Id as Id,DriversData.Name,CabsData.VehicleNumber,DriversData.PhoneNumber from CabsData
join DriversData on DriversData.Id=CabsData.DriverId) as E on E.Id=CabBookingsData.CabId
where RosterId=@RosterId;
";



                    var response = await connection.QueryAsync<CabBookingRosterInfo>(sqlQuery, new { RosterId = id });

                    return response.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }


    }
}
