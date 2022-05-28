namespace diplomski_backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public string Tags { get; set; }
        public int State { get; set; }
        public float Price { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
    }
}