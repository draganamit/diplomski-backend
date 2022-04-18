using System.Collections.Generic;
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

       [HttpGet("GetAll")]
       public async Task<IActionResult> GetAllUsers()
       {
           ServiceResponse<List<GetUserDto>> response = await _authRepo.GetAllUsers();
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }

       [HttpGet("{id}")]
       public async Task<IActionResult> GetUserById(int id)
       {
           ServiceResponse<GetUserDto> response = await _authRepo.GetUserById(id);
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }

       [HttpPut]
       public async Task<IActionResult> UpdateUser(UpdateUserDto updatedUser)
       {
           ServiceResponse<GetUserDto> response = await _authRepo.UpdateUser(updatedUser);
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }

        [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUser(int id)
       {
           ServiceResponse<List<GetUserDto>> response = await _authRepo.DeleteUser(id);
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }

       [HttpPut("UpdateByUser")]
       public async Task<IActionResult> UpdateUserByUser(UpdateUserDto updateUserDto)
       {
           ServiceResponse<GetUserDto> response = await _authRepo.UpdateUserByUser(updateUserDto);
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }

       [HttpDelete("DeleteByUser")]
       public async Task<IActionResult> DeleteUserByUser()
       {
           ServiceResponse<List<GetUserDto>> response = await _authRepo.DeleteUserByUser();
           if(response.Data == null)
           {
               return NotFound(response);
           }
           return Ok(response);
       }
    }
}