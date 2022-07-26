using System;
using System.ComponentModel.DataAnnotations;

namespace diplomski_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public User UserBuyer { get; set; }
        public bool Confirm { get; set; } = false;
        public string Telephone { get; set; }
        public string Address { get; set; }

        public DateTime Date { get; set; }
        public string BuyerNote { get; set; }
        public string SellerNote { get; set; }

    }
}