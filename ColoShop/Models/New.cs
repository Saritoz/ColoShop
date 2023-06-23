using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class New
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
