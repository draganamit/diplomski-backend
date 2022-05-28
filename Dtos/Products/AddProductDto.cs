using diplomski_backend.Dtos.Categories;
using diplomski_backend.Models;

namespace diplomski_backend.Dtos.Products
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public float Price { get; set; }

        public GetCategoryDto Category { get; set; }
    }
}