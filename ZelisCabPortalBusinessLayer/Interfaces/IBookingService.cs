using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZelisCabPortalCoreLayer.Models;

using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Interfaces
{
    public interface IBookingService
    {
        public Task<List<Employee>> getAllEmployees();
        public Task<List<Cabs>> getAllCabs();
        public Task<List<Drivers>> getAllDrivers();
        public Task<List<CabBookings>> getAllBookings();
        public Task<List<Cabs>> getAllAvailableCabs();
        public Task<Boolean> bookACab(List<RosterInfoRequest> request, int rosterid);
        public Task<Boolean> ProcessBooking(ProcessBookRequest request);

        public Task<List<BookingRecord>> getCurrentWeekBookings(int employeeid);
        public Task<List<RosterRequest>> getpendingrosters();
        public Task<Boolean> ApproveRosterRequest(RosterRequest Request, CabData cabdata);
        public Task<Boolean> RejectRosterRequest(RosterRequest Request);
        public Task<List<CurrentDayBooking>> DateWiseBookings(DateTime date,string vehicleno);
        public Task<List<CabBookingRosterInfo>> getRosterBookingsById(int id);
    }
}
