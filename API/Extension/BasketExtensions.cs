using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class BasketExtensions
{
    public static BasketDto ToDto(this Basket basket)
    {
        ArgumentNullException.ThrowIfNull(basket);

        return new BasketDto
        {
            Id = basket.Id,
            BasketId = basket.BasketId,
            PaymentIntentId = basket.PaymentIntentId,
            ClientSecret = basket.ClientSecret,
            Items = [.. basket.Items.Select(item => new BasketItemDto
      {
        ProductId = item.ProductId,
        Name = item.Product.Name,
        Price = item.Product.Price,
        PictureUrl = item.Product.PictureUrl,
        Brand = item.Product.Brand,
        Type = item.Product.Type,
        Quantity = item.Quantity
      })]
        };
    }

    public static async Task<Basket> GetBasketWithItems(this IQueryable<Basket> query,
        string? basketId)
    {
        return await query
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.BasketId == basketId)
            ?? throw new Exception("Cannot get basket");
    }
}
