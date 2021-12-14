using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTSeminar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public UserController(ILogger<UserController> logger, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody]UserCrendetial user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user);
            if (token==null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}