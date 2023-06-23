using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ColoShop.Models
{
    public class ViewCount
    {
        public long Today { get; set; }
        public long Yesterday { get; set; }
        public long ThisWeek { get; set; }
        public long LastWeek { get; set; }
        public long ThisMonth { get; set; }
        public long LastMonth { get; set; }
        public long Total { get; set; }
    }

    public class ViewStatistic
    {
        private readonly EcommerceShopContext _context = new EcommerceShopContext();
        public ViewStatistic() { }

        public ViewCount GetStatisticView()
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            var result = connection.QueryFirstOrDefault<ViewCount>("product_statistic", commandType: CommandType.StoredProcedure);
            return result;

        }
    }
}
