using System.Threading.Tasks;
using diplomski_backend.Data;
using diplomski_backend.Dtos;
using diplomski_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace diplomski_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

       [HttpPost("Register")]
       public async Task<IActionResult> Register(UserRegistrationDto request)
       {
           ServiceResponse<int> response = await _authRepo.Register(
               new User { Name = request.Name, Surname = request.Surname, Email = request.Email, Location = request.Location},
               request.Password
           );
           if(!response.Success)
           {
               return BadRequest(response);
           }
           return Ok(response);
       }
        [HttpPost("Login")]
       public async Task<IActionResult> Login(UserLoginDto request)
       {
           ServiceResponse<string> response = await _authRepo.Login(request.Email, request.Password);
           if(!response.Success)
           {
               return BadRequest(response);
           }
           return Ok(response);
       }

    }
}