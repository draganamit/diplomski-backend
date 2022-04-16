using System.Collections.Generic;

namespace diplomski_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public TypeUser Type { get; set; } = TypeUser.RegUser;
        public List<Product> Products { get; set; }
    }
}