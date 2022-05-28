using diplomski_backend.Dtos.Categories;

namespace diplomski_backend.Dtos.Products
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public float Price { get; set; }
        public GetCategoryDto Category { get; set; }
    }
}