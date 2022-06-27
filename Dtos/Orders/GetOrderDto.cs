using diplomski_backend.Dtos.Products;

namespace diplomski_backend.Dtos.Orders
{
    public class GetOrderDto
    {
        public GetProductWithUserDto Product { get; set; }
        public GetUserDto UserBuyer { get; set; }
        public bool Confirm { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }


    }
}