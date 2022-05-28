using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos;
using diplomski_backend.Models;

namespace diplomski_backend.Data
{
    public interface IAuthRepository
    {
        public Task<ServiceResponse<int>> Register(User user, string password);
        public Task<ServiceResponse<string>> Login(string email, string password);
        public Task<ServiceResponse<List<GetUserWithProductDto>>> GetAllUsers();
        public Task<ServiceResponse<GetUserWithProductDto>> GetUserById(int id);
        public Task<ServiceResponse<GetUserWithProductDto>> UpdateUser(UpdateUserDto updatedUser);
        public Task<ServiceResponse<List<GetUserWithProductDto>>> DeleteUser(int id);
        public Task<ServiceResponse<GetUserWithProductDto>> UpdateUserByUser(UpdateUserDto updatedUser);
        public Task<ServiceResponse<List<GetUserWithProductDto>>> DeleteUserByUser();
        public Task<ServiceResponse<GetUserWithProductDto>> GetUserByUser();

        public Task UpdatePassword(string oldPassword, string newPassword);
    }
}