using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Subscribe
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
