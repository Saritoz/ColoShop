using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? PaymentType { get; set; }
        public string? Status { get; set; }

        public virtual User? UsernameNavigation { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
