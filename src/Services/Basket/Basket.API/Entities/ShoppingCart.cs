using System.Collections.Generic;
namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice 
        {
            get
            {
                var total = 0m;
                foreach (var item in Items)
                {
                    total += item.Quantity * item.Price;
                }
                return total;
            }
        }
    }
}
