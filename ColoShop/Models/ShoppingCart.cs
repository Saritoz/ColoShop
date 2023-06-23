namespace ColoShop.Models
{
    public class ShoppingCart
    {
        // singleton Pattern
        private static readonly ShoppingCart instance = new ShoppingCart();
        public static ShoppingCart Instance => instance;
        public List<ShoppingCartItem> Items { get; } = new List<ShoppingCartItem>();
        private ShoppingCart()
        {

        }

        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var existInCart = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existInCart != null)
            {
                existInCart.Quantity += quantity;
                existInCart.TotalPrice = existInCart.Price * existInCart.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public bool RemoveFromCart(int id)
        {
            var existInCart = Items.SingleOrDefault(x => x.ProductId == id);
            if (existInCart != null)
            {
                return Items.Remove(existInCart);
            }
            return false;
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var existInCart = Items.SingleOrDefault(x => x.ProductId == id);
            if (existInCart != null)
            {
                existInCart.Quantity = quantity;
                existInCart.TotalPrice = existInCart.Price * existInCart.Quantity;
            }
        }

        public decimal GetTotal()
        {
            return Items.Sum(x => x.TotalPrice);
        }

        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }

        public void ClearCart()
        {
            Items.Clear();
        }
    }


    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string ProductImage { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
