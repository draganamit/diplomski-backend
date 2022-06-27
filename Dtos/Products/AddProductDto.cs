using System.Collections.Generic;
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
        public List<string> Images { get; set; } = new List<string>();
        public int CategoryId { get; set; }
        public string[] Tags { get; set; }

    }
}