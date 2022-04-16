using System.Threading.Tasks;
using diplomski_backend.Models;

namespace diplomski_backend.Data
{
    public interface IAuthRepository
    {
         Task<ServiceResponse<int>> Register(User user, string password);
         Task<ServiceResponse<string>> Login(string email, string password);
    }
}