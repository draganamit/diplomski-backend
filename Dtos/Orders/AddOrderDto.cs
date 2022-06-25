namespace diplomski_backend.Dtos.Orders
{
    public class AddOrderDto
    {
        public int ProductId { get; set; }
        //public int UserSellerId { get; set; }
        //public int UserBuyerId { get; set; }
        public bool Confirm { get; set; }
    }
}