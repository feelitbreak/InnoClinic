using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Repositories;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository _userRep;

        public AuthorizationController(IUserRepository userRep)
        {
            _userRep = userRep;
        }

        [HttpPost(Name = "SignUp")]
        public IActionResult Post([FromBody] User userSignUp)
        {
            if (userSignUp is null)
            {
                return BadRequest();
            }

            if (!userSignUp.Password.Equals(userSignUp.ReenteredPassword))
            {
                return BadRequest("The passwords you’ve entered don’t coincide");
            }

            _userRep.AddUser(userSignUp);

            return Ok();
        }
    }
}