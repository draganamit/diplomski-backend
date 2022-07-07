using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using diplomski_backend.Data;
using diplomski_backend.Dtos;
using diplomski_backend.Models;
using diplomski_backend.Services.MailService;
using Microsoft.AspNetCore.Mvc;


namespace diplomski_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMailService _mailService;

        public AuthController(IAuthRepository authRepo, IMailService mailService)
        {
            _authRepo = authRepo;
            _mailService = mailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Name = request.Name, Surname = request.Surname, Email = request.Email, Location = request.Location },
                request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authRepo.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            ServiceResponse<List<GetUserWithProductDto>> response = await _authRepo.GetAllUsers();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            ServiceResponse<GetUserWithProductDto> response = await _authRepo.GetUserById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            ServiceResponse<GetUserWithProductDto> response = await _authRepo.BlockUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserWithProductDto> response = await _authRepo.UpdateUser(updatedUser);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserWithProductDto>> response = await _authRepo.DeleteUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateByUser")]
        public async Task<IActionResult> UpdateUserByUser(UpdateUserDto updateUserDto)
        {
            ServiceResponse<GetUserWithProductDto> response = await _authRepo.UpdateUserByUser(updateUserDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteByUser")]
        public async Task<IActionResult> DeleteUserByUser()
        {
            ServiceResponse<List<GetUserWithProductDto>> response = await _authRepo.DeleteUserByUser();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatedPassword)
        {
            return Ok(await _authRepo.UpdatePassword(updatedPassword.OldPassword, updatedPassword.NewPassword));
        }

        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetUserByUser()
        {
            ServiceResponse<GetUserWithProductDto> response = await _authRepo.GetUserByUser();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> SendEmailAsync(MailRequestDto mailRequest)
        {
            string password = await _authRepo.ResetPassword(mailRequest.Email);
            if (password == null)
            {
                return BadRequest();
            }
            await _mailService.SendEmailAsync(mailRequest, password);

            return Ok();
        }


    }
}