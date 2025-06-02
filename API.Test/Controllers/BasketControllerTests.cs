using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Controllers;
using API.Entities;
using API.DTOs;
using API.Data;

namespace API.Test.Controllers
{
    public class BasketControllerTests
    {
        private readonly Mock<DbSet<Product>> _productSetMock;
        private readonly Mock<DbSet<Basket>> _basketSetMock;
        private readonly Mock<StoreContext> _contextMock;
        private readonly BasketController _controller;

        public BasketControllerTests()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().Options;
            _contextMock = new Mock<StoreContext>(options);
            _productSetMock = new Mock<DbSet<Product>>();
            _basketSetMock = new Mock<DbSet<Basket>>();

            _contextMock.Setup(x => x.Products).Returns(_productSetMock.Object);

            _controller = new BasketController(_contextMock.Object);

            // Optionally set up User.Identity if needed
        }

        [Fact]
        public async Task GetBasket_ReturnsNoContent_WhenBasketIsNull()
        {
            // Arrange
            var controller = new TestableBasketController(_contextMock.Object);
            controller.MockRetrieveBasket(null);

            // Act
            var result = await controller.GetBasket();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        //[Fact]
        //public async Task GetBasket_ReturnsBasketDto_WhenBasketExists()
        //{
        //    // Arrange
        //    var basket = new Basket { Id = 1 };
        //    var dto = new BasketDto { Id = 1 };

        //    var controller = new TestableBasketController(_contextMock.Object);
        //    controller.MockRetrieveBasket(basket);
        //    controller.MockToDto(basket, dto);

        //    // Act
        //    var result = await controller.GetBasket();

        //    // Assert
        //    var okResult = Assert.IsType<ActionResult<BasketDto>>(result);
        //    Assert.Equal(dto.Id, okResult.Value!.Id);
        //}

        //[Fact]
        //public async Task AddItemToBasket_ReturnsCreatedAtAction_WhenItemAddedSuccessfully()
        //{
        //    // Arrange
        //    var product = new Product { Id = 1, Name = "Test" };
        //    var basket = new Mock<Basket>();
        //    var dto = new BasketDto { Id = 123 };

        //    var controller = new TestableBasketController(_contextMock.Object);
        //    controller.MockRetrieveBasket(basket.Object);
        //    controller.MockToDto(basket.Object, dto);

        //    _contextMock.Setup(x => x.Products.FindAsync(1))
        //        .ReturnsAsync(product);

        //    _contextMock.Setup(x => x.SaveChangesAsync(default))
        //        .ReturnsAsync(1);

        //    // Act
        //    var result = await controller.AddItemToBasket(1, 1);

        //    // Assert
        //    var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        //    var returnedDto = Assert.IsType<BasketDto>(created.Value);
        //    Assert.Equal(dto.Id, returnedDto.Id);
        //}

        [Fact]
        public async Task RemoveBasketItem_ReturnsOk_WhenItemRemovedSuccessfully()
        {
            // Arrange
            var basket = new Mock<Basket>();
            var controller = new TestableBasketController(_contextMock.Object);
            controller.MockRetrieveBasket(basket.Object);

            _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await controller.RemoveBasketItem(1, 1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }

    public class TestableBasketController : BasketController
    {
        private Basket _basketToReturn;
        private BasketDto _dtoToReturn;

        public TestableBasketController(StoreContext context) : base(context) { }

        public void MockRetrieveBasket(Basket basket) => _basketToReturn = basket;
        public void MockToDto(Basket basket, BasketDto dto) => _dtoToReturn = dto;

        //protected override Task<Basket?> RetrieveBasket() => Task.FromResult(_basketToReturn);

        //protected override BasketDto ToDto(Basket basket) => _dtoToReturn;
    }

}
