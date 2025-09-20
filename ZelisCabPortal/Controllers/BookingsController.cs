using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private IBookingService _booking { get; }

        public BookingsController(IBookingService booking)
        {
            _booking = booking;
        }
        [HttpGet("getAllBookings")]
        public async Task<ActionResult<List<Employee>>> getAllEmployees()
        {
            try
            {
                List<Employee> response = await _booking.getAllEmployees();
                return Ok(response);
            }
            catch (Exception) { throw; }


        }
        [HttpGet("getAllDrivers")]
        public async Task<ActionResult<List<Drivers>>> getAllDrivers()
        {
            try
            {
                List<Drivers> response = await _booking.getAllDrivers();
                return Ok(response);
            }
            catch (Exception) { throw; }


        }
        [HttpGet("getAllCabs")]
        public async Task<ActionResult<List<Cabs>>> getAllCabs()
        {
            try
            {
                List<Cabs> response = await _booking.getAllCabs();
                return Ok(response);
            }
            catch (Exception) { throw; }


        }
        [HttpPost("bookAcab")]
        public async Task<ActionResult<Boolean>> Book([FromBody] WeeklyBookings request)
        {
            try
            {
                Boolean response = await _booking.bookACab(request.rosterlist,request.RosterId);
                if (response)
                {
                    return Ok(true);
                }
            }
            catch (Exception ex) { }
            return Ok(false);
        }
        [HttpGet("getcurrentroster")]
        public async Task<ActionResult<List<BookingRecord>>> GetCurrentRoster(int employeeId)
        {
            try
            {
                List<BookingRecord> response = await _booking.getCurrentWeekBookings(employeeId);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    // If response is null, return a 404 Not Found
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you should use a proper logging mechanism)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }

        }
        [HttpGet("getpendingrosters")]
        public async Task<ActionResult<List<RosterRequest>>> GetPendingRostersAdmin()
        {
            try
            {
                List<RosterRequest> response = await _booking.getpendingrosters();

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    // If response is null, return a 404 Not Found
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you should use a proper logging mechanism)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }

        }
        [HttpPost("approverosterrequest")]
        public async Task<ActionResult<Boolean>> Approve([FromBody] AdminProcessBookingRequest request)
        {
            try
            {
                Boolean response = await _booking.ApproveRosterRequest(request.Request,request.CabInfo);

                if (response)
                {
                    return Ok(response);
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception (you should use a proper logging mechanism)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }

        }
        [HttpPost("rejectrosterrequest")]
        public async Task<ActionResult<Boolean>> Reject([FromBody] RosterRequest request)
        {
            try
            {
                Boolean response = await _booking.RejectRosterRequest(request);

                if (response)
                {
                    return Ok(response);
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception (you should use a proper logging mechanism)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }

        }
        [HttpGet("getdaywisebookings")]
        public async Task<ActionResult<Boolean>> GetDayWiseBookings(DateTime date,string vehicleno)
        {
            try
            {
                List<CurrentDayBooking> response = await _booking.DateWiseBookings(date,vehicleno);

                if (response!=null)
                {
                    return Ok(response);
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception (you should use a proper logging mechanism)
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }

        }
        [HttpGet("getrosterbookingbyid")]
        public async Task<ActionResult<List<CabBookingRosterInfo>>> GetRosterInfoById(int id)
        {
            try
            {
                List<CabBookingRosterInfo> response = await _booking.getRosterBookingsById(id);

                if (response != null && response.Any())
                {
                    return Ok(response);
                }

                // If no results are found, return a 404 Not Found
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception using a proper logging mechanism
                Console.WriteLine($"Exception: {ex.Message}");

                // Return a 500 Internal Server Error
                return StatusCode(500, "An unexpected error occurred");
            }
        }





    }
}
