using System;
using System.Collections.Generic;

namespace ColoShop.Models
{
    public partial class Statistic
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public long? ViewCount { get; set; }
    }
}
