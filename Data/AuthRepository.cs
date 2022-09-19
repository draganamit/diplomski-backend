using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using diplomski_backend.Dtos;
using diplomski_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace diplomski_backend.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.User.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null || user.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordsalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordsalt;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;
            return response;

        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.User.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }

        public async Task<ServiceResponse<List<GetUserWithProductDto>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserWithProductDto>> response = new ServiceResponse<List<GetUserWithProductDto>>();
            List<User> users = await _context.User.Include(u => u.Products).ToListAsync();
            response.Data = _mapper.Map<List<GetUserWithProductDto>>(users).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetUserWithProductDto>> GetUserById(int id)
        {
            ServiceResponse<GetUserWithProductDto> response = new ServiceResponse<GetUserWithProductDto>();
            User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == id);
            response.Data = _mapper.Map<GetUserWithProductDto>(user);
            return response;
        }

        public async Task<ServiceResponse<GetUserWithProductDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserWithProductDto> response = new ServiceResponse<GetUserWithProductDto>();
            try
            {
                User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Location = updatedUser.Location;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUserWithProductDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetUserWithProductDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserWithProductDto>> response = new ServiceResponse<List<GetUserWithProductDto>>();
            try
            {
                User user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                List<User> users = await _context.User.Include(u => u.Products).ToListAsync();
                response.Data = _mapper.Map<List<GetUserWithProductDto>>(users).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserWithProductDto>> UpdateUserByUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserWithProductDto> response = new ServiceResponse<GetUserWithProductDto>();
            try
            {
                // User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == GetUserId());

                User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Location = updatedUser.Location;
                _context.User.Update(user);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetUserWithProductDto>(user);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetUserWithProductDto>>> DeleteUserByUser()
        {
            ServiceResponse<List<GetUserWithProductDto>> response = new ServiceResponse<List<GetUserWithProductDto>>();
            try
            {
                User user = await _context.User.FirstOrDefaultAsync(x => x.Id == GetUserId());
                _context.User.Remove(user);
                await _context.SaveChangesAsync();

                List<User> users = await _context.User.Include(u => u.Products).ToListAsync();
                response.Data = _mapper.Map<List<GetUserWithProductDto>>(users).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Boolean> UpdatePassword(string oldPassword, string newPassword, int userId)
        {

            //User user = await _context.User.FirstOrDefaultAsync(x => x.Id == GetUserId());
            User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);

            if (VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        async public Task<ServiceResponse<GetUserWithProductDto>> GetUserByUser()
        {
            ServiceResponse<GetUserWithProductDto> response = new ServiceResponse<GetUserWithProductDto>();
            try
            {
                User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == GetUserId());

                response.Data = _mapper.Map<GetUserWithProductDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        async public Task<ServiceResponse<GetUserWithProductDto>> BlockUser(int id)
        {
            ServiceResponse<GetUserWithProductDto> response = new ServiceResponse<GetUserWithProductDto>();
            User user = await _context.User.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == id);
            user.IsDeleted = !user.IsDeleted;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetUserWithProductDto>(user);
            return response;

        }
        public async Task<string> ResetPassword(string email)
        {
            string password = CreatePassword();
            bool userExist = await UserExists(email);
            if (!userExist)
            {
                return null;
            }
            User user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return password;


        }
        private string CreatePassword()
        {
            int length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}