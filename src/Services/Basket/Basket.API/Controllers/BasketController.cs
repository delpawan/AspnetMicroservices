using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet("{userName}", Name = nameof(GetBasket))]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            return await _basketRepository.UpdateBasket(shoppingCart);
        }

        [HttpDelete("{userName}", Name = nameof(DeleteBasket))]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
        }
    }
}
