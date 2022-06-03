namespace diplomski_backend.Dtos.Products
{
    public class ProductSearchModel
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int? CategoryId { get; set; }
        public float? PriceFrom { get; set; }
        public float? PriceTo { get; set; }

        public string Location { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }

    }
}