using System.Collections.Generic;
using diplomski_backend.Dtos.Products;

namespace diplomski_backend.Dtos
{
    public class GetUserWithProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public List<GetProductDto> Products { get; set; }
        public int Type { get; set; }

    }
}