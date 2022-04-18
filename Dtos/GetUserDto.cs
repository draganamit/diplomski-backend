using System.Collections.Generic;
using diplomski_backend.Models;

namespace diplomski_backend.Dtos
{
    public class GetUserDto
    {
         public int Id { get; set; }
         public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
         public List<Product> Products { get; set; }
    }
}