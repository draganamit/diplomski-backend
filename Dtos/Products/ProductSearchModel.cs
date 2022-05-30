namespace diplomski_backend.Dtos.Products
{
    public class ProductSearchModel
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int? CategoryId { get; set; }

    }
}