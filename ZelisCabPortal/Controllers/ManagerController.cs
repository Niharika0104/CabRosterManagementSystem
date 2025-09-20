using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalCoreLayer.Models;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManager _manager { get; }

        public ManagerController(IManager manager)
        {
            _manager = manager;
        }

        [HttpPost("getPendingApprovals")]
        public async Task<ActionResult<List<Employee>>> getPendingApprovals([FromBody] int employeeid)
        {
            try
            {
                List<Employee> list =await _manager.PendingApprovals(employeeid);
                return Ok(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("GetAllEmloyees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees([FromBody] int employeeid)
        {
            try
            {
                List<Employee> list = await _manager.GetAllEmployees(employeeid);
                return Ok(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("process")]
        public async Task<ActionResult<Boolean>> processRequest([FromBody] ProcessDTO request)
        {
            try
            {
             Boolean response = await _manager.ProcessRequest(request);
                if (response) return true;
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
