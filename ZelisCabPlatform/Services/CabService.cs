using Azure.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using ZelisCabPlatform.Interfaces;
using ZelisCabPlatform.Models;
using ZelisCabPortalCoreLayer.Models;

namespace ZelisCabPlatform.Services
{
    public class CabService
    {
        private readonly HttpClient _httpClient;
        private readonly LoginService _loginservice;


        public CabService(HttpClient httpClient,LoginService loginService)
        {
            _httpClient = httpClient;
            _loginservice = loginService;
        }
        public async Task<Boolean> RaiseRequest(int employeeId,int type)
        {
            string url = type == 4 ? "CabService/initiate" : "CabService/terminate";
            try
            {
                var res = await _httpClient.PostAsJsonAsync<int>(url, employeeId);
               
                bool value=await res.Content.ReadFromJsonAsync<bool>();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        public async Task<RosterRequest> CreateRoster(List<RosterInfoRequest> requests,DateTime startdate)
        {
            string url = "CabService/createroster" ;
            try
            {
                RosterRequest body = new RosterRequest() {
                    EmployeeId = _loginservice?.employee?.EmployeeId, 
                    StartDate = startdate, 
                    EndDate = startdate.AddDays(7) ,Status=1,
                    AppliedDate=DateTime.Now.Date
                };
                var res = await _httpClient.PostAsJsonAsync<RosterRequest>(url,body);
                if (res.IsSuccessStatusCode)
                {
                    RosterRequest value = await res.Content.ReadFromJsonAsync<RosterRequest>();
                    if (value != null) return value;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public async Task<Boolean> WeeklyBookings(List<RosterInfoRequest> requests, int rosterid)
        {
            string url = "Bookings/bookAcab";
            try
            {
                var res = await _httpClient.PostAsJsonAsync<WeeklyBookings>(url, new WeeklyBookings() { rosterlist=requests,RosterId=rosterid });

                Boolean value = await res.Content.ReadFromJsonAsync<Boolean>();
                if (value)
                    return value;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        public async Task<List<RosterRequest>> GetRejectedRosters(int? id)
        {
            string url = "CabService/getAllRoster?employeeId=" + id;
            try
            {
                List<RosterRequest> list = await _httpClient.GetFromJsonAsync<List<RosterRequest>>(url);
               return list?.Where(r => r.Status == 3)?.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<RosterRequest>> GetApprovedRosters(int? id)
        {
            string url = "CabService/getAllRoster?employeeId=" + id;
            try
            {
                List<RosterRequest> list = await _httpClient.GetFromJsonAsync<List<RosterRequest>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<BookingRecord>> getcurrentroster(int? id)
        {
            string url = "Bookings/getcurrentroster?employeeId=" + id;
            try
            {
                List<BookingRecord> list = await _httpClient.GetFromJsonAsync<List<BookingRecord>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<DriverData>> getDriversData()
        {
            string url = "CabService/getAllDrivers";
            try
            {
                List<DriverData> list = await _httpClient.GetFromJsonAsync<List<DriverData>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<CabData>> getCabsData()
        {
            string url = "CabService/getAllCabs";
            try
            {
                List<CabData> list = await _httpClient.GetFromJsonAsync<List<CabData>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
            public async Task<List<RosterRequest>> getpendingrosters()
            {
                string url = "Bookings/getpendingrosters";
                try
                {
                    List<RosterRequest> list = await _httpClient.GetFromJsonAsync<List<RosterRequest>>(url);
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        public async Task<Boolean> Approve(RosterRequest roster,CabData cabData)
            {
                string url = "Bookings/approverosterrequest";
            AdminProcessBookingRequest request = new AdminProcessBookingRequest() { Request = roster, CabInfo = cabData };
                try
                {
                   var response = await _httpClient.PostAsJsonAsync<AdminProcessBookingRequest>(url,request);
                if (response.IsSuccessStatusCode)
                {
                    Boolean res = await response.Content.ReadFromJsonAsync<Boolean>();
                    if (res) return true;
                }
                    return false;
                }
                catch (Exception)
                {

                    throw;
                }
            } 
        public async Task<Boolean> Reject(RosterRequest roster)
            {
                string url = "Bookings/rejectrosterrequest";
        
                try
                {
                   var response = await _httpClient.PostAsJsonAsync<RosterRequest>(url,roster);
                 if (response.IsSuccessStatusCode)
                {
                    Boolean res = await response.Content.ReadFromJsonAsync<Boolean>();
                    if (res) return true;
                }
                    return false;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        
    public async Task<List<CurrentDayBooking>> GetDateWiseBookings(DateTime date,string vehicleno)
    {
        string url = "Bookings/getdaywisebookings?date="+date.ToString("yyyy-MM-dd")+"&vehicleno="+vehicleno;

       
            try
            {
                List<CurrentDayBooking> list = await _httpClient.GetFromJsonAsync<List<CurrentDayBooking>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
           
          
       
    }
        public async Task<List<Employee>> GetAllEmployeesUsingCabService()
        {
            string url = "CabService/getAllEmployeesUsingCabService";


            try
            {
                List<Employee> list = await _httpClient.GetFromJsonAsync<List<Employee>>(url);
                return list;
            }
            catch (Exception)
            {

                throw;
            }



        }
        public async Task<int> GetRequestCount(DateTime date,int id)
        {
            string url = "CabService/getRequestCount?startdate="+date+"&employeeid="+id;


            try
            {
               int count = await _httpClient.GetFromJsonAsync<int>(url);
                return count;
            }
            catch (Exception)
            {

                throw;
            }



        }
        public async Task<List<CabBookingRosterInfo>> GetRosterInfoByIdWithCabDetails(int id)
        {
            string url = "Bookings/getrosterbookingbyid?id=" + id;


            try
            {
                List<CabBookingRosterInfo> list = await _httpClient.GetFromJsonAsync<List<CabBookingRosterInfo>>(url);
               
                return list;
            }
            catch (Exception)
            {

                throw;
            }



        }
        public async Task<List<RosterInfo>> GetRosterInfoById( int id)
        {
            string url = "Bookings/getrosterbookingbyid?id=" + id;


            try
            {
                List<CabBookingRosterInfo> list = await _httpClient.GetFromJsonAsync<List<CabBookingRosterInfo>>(url);
                List<RosterInfo> rosterlist = new List<RosterInfo>();
                foreach(CabBookingRosterInfo item in list)
                {
                    rosterlist.Add(new RosterInfo() { 
                    pickup=item.PickUpLocation,
                    drop=item.DropLocation,
                    dateofbooking=item.BookingDate,
                    pickupTime=item.PickUpTime ,
                    shift=item.Shift
                    });
                }
                return rosterlist;
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
