using API.Data;
using API.DTOs;
using API.Entities;
using API.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController(StoreContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NoContent();
            return basket.ToDto();

        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity = 1)
        {
            var basket = await RetrieveBasket();
            basket ??= CreateBasket();
            var product = await context.Products.FindAsync(productId);
            if (product == null) return BadRequest("Problem while adding item to basket");
            basket.AddItem(product, quantity);
            var result = await context.SaveChangesAsync() > 0;
            if (result) return CreatedAtAction(nameof(GetBasket), basket.ToDto());
            return BadRequest("Problem while adding item to basket");
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity = 1)
        {
            var basket = await RetrieveBasket();
            if (basket == null) return BadRequest("Unable to retrieve basket");

            basket.RemoveItem(productId, quantity);

            var result = await context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest("Problem while removing item from basket");
        }

        #region Private Methods
        private Basket? CreateBasket()
        {
            var basketId = Guid.NewGuid().ToString();
            var cookiesOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                IsEssential = true
            };
            Response.Cookies.Append("basketId", basketId, cookiesOptions);
            var basket = new Basket
            {
                BasketId = basketId
            };
            context.Baskets.Add(basket);
            return basket;
        }

        private async Task<Basket?> RetrieveBasket()
        {
            return await context.Baskets
                      .Include(x => x.Items)
                      .ThenInclude(x => x.Product)
                      .FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);
        }
        #endregion


    }
}
