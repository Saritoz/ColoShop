using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Position { get; set; }
    }
}
