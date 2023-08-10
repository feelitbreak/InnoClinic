using Microsoft.AspNetCore.Mvc;
using Infrastructure_Layer;
using Domain_Layer.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ClinicDbContext _context;

        public AuthorizationController(ClinicDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "SignUp")]
        public IActionResult Post([FromBody] User userSignUp)
        {
            string str = null;
            return Ok();
        }
    }
}