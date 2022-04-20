using diplomski_backend.Models;

namespace diplomski_backend.Dtos.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public Category Category { get; set; }
    }
}