using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class ProductOrder
    {
        public int Id { get; set; }
        public int? IdOrder { get; set; }
        public int? IdProduct { get; set; }
        public int? Amount { get; set; }

        public virtual Order? IdOrderNavigation { get; set; }
        public virtual Product? IdProductNavigation { get; set; }
    }
}
