using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache 
                ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basketString = await _distributedCache.GetStringAsync(userName);

            return string.IsNullOrEmpty(basketString) ? null :
                JsonConvert.DeserializeObject<ShoppingCart>(basketString);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            var basketString = JsonConvert.SerializeObject(shoppingCart);
            
            await _distributedCache.SetStringAsync(shoppingCart.UserName, basketString);

            return shoppingCart;
        }

        public async Task DeleteBasket(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }
    }
}
