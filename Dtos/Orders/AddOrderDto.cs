namespace diplomski_backend.Dtos.Orders
{
    public class AddOrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Confirm { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
    }
}