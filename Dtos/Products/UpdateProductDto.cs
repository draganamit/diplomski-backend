using System.Collections.Generic;
using diplomski_backend.Models;

namespace diplomski_backend.Dtos.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public float Price { get; set; }
        public string[] Tags { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public int CategoryId { get; set; }

    }
}