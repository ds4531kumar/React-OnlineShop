using API.DTOs;
using API.Entities;

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
}
