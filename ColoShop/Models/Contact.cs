using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
