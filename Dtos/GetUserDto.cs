using System.Collections.Generic;
using diplomski_backend.Dtos.Products;
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
        public int Type { get; set; }

        //Ovo pravi beskonacnu petlju kad se mapira Product u GetProductDto
        // public List<GetProductDto> Products { get; set; }
    }
}