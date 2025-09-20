using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalDataLayer.Models;
using ZelisCabPortalCoreLayer.Models;

namespace ZelisCabPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuth _auth { get; }

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

       
        [HttpPost("login")]
        public async Task<ActionResult<Employee>> Login([FromBody] LoginInfo login)
        {
            try
            {
                Employee response=await _auth.Login(login.Email,login.Password);
               

                // Return a successful response with the token
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return NotFound();
            }
        }
    }
}
