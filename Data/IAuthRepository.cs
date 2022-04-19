using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos;
using diplomski_backend.Models;

namespace diplomski_backend.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register(User user, string password);
         Task<ServiceResponse<string>> Login(string email, string password);
         Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
         Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUserByUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUserByUser();
        Task UpdatePassword(string oldPassword,string newPassword);
    }
}