using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabServiceController : ControllerBase

    {

        private ICabService _cab { get; }

        public CabServiceController(ICabService cab)
        {
            _cab = cab;
        }
        [HttpPost("initiate")]
        public async Task<ActionResult<Boolean>> availService([FromBody]int employeeId)
        {
            try
            {
                Boolean response = await _cab.AvailService(employeeId);
                if (response)
                {
                    return Ok(true);
                }
            }
            catch(Exception ex) { }
            return Ok(false);
        }
        [HttpPost("terminate")]
        public async Task<ActionResult<Boolean>> TerminateService([FromBody] int employeeId)
        {
            try
            {
                Boolean response = await _cab.TerminateService(employeeId);
                if (response)
                {
                    return Ok(true);
                }
            }
            catch (Exception ex) { }
            return Ok(false);
        }
        [HttpPost("createroster")]
        public async Task<ActionResult<RosterRequest>> CreateRoster([FromBody] RosterRequest request)
        {
            try
            {
                RosterRequest response = await _cab.createRoster(request);
                if (response!=null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return null;
        }
        [HttpGet("getAllRoster")]
        public async Task<ActionResult<List<RosterRequest>>> GetAllRosters( int employeeId )
        {
            try
            {
                List<RosterRequest> response = await _cab.GetAllRosters(employeeId);
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return Ok(null);
        }
        [HttpGet("getAllDrivers")]
        public async Task<ActionResult<List<DriverData>>> getAllDrivers()
        {
            try
            {
                List<DriverData> response = await _cab.GetAllDrivers();
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return Ok(null);
        }
        [HttpGet("getAllCabs")]
        public async Task<ActionResult<List<CabData>>> getAllCAbs()
        {
            try
            {
                List<CabData> response = await _cab.GetAllCabs();
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return Ok(null);
        }
        [HttpGet("getAllEmployeesUsingCabService")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployeesUsingCabService()
        {
            try { 
          
                List<Employee> response = await _cab.GetAllEmployees();
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return Ok(null);
        }
        [HttpGet("getRequestCount")]
        public async Task<ActionResult<int>> GetRequestCount(DateTime startdate,int employeeid)
        {
            try
            {

                int response = await _cab.GetRequestCount(startdate,employeeid);
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex) { }
            return Ok(null);
        }
    }
}
