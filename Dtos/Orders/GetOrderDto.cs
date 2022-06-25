using diplomski_backend.Dtos.Products;

namespace diplomski_backend.Dtos.Orders
{
    public class GetOrderDto
    {
        public GetProductWithUserDto Product { get; set; }
        public GetUserDto UserBuyerId { get; set; }
        public bool Confirm { get; set; }


    }
}