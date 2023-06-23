using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
