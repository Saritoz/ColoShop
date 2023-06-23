using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int? Quantity { get; set; }
        public int? IdCategoryProduct { get; set; }
        public short? Avatar { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHome { get; set; }
        public bool IsHot { get; set; }
        public bool IsSale { get; set; }
        public string? ProductionCode { get; set; }
        public int? ViewCount { get; set; }

        public virtual ProductCategory? IdCategoryProductNavigation { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
