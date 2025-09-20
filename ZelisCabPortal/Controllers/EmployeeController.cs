using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee _employee { get; }

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }
        [HttpGet()]
        public async  Task<ActionResult<Employee>>getEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employee.GetEmployeeById(id);
                if (employee == null) return BadRequest();
                return Ok(employee);
            }
            catch (Exception) { }
            return new Employee();
        }
    }
}
