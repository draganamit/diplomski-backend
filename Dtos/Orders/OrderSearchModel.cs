namespace diplomski_backend.Dtos.Orders
{
    public class OrderSearchModel
    {
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public string Sale { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }
}