namespace diplomski_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        //public User UserSeller { get; set; }
        public User UserBuyer { get; set; }
        public bool Confirm { get; set; } = false;
    }
}