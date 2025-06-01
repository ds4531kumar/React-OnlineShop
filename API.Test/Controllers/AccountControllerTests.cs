using API.Controllers;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
namespace API.Test.Controllers
{


    public class AccountControllerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _userManagerMock = GetMockUserManager();
            _userManagerMock
                .Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock
                .Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "Member"))
                .ReturnsAsync(IdentityResult.Success);

            _signInManagerMock = GetMockSignInManager(_userManagerMock.Object);
            _controller = new AccountController(_signInManagerMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123!" };

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenUserCreationFails()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "",
                Password = ""
            };

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            Assert.Equal(400, (result as StatusCodeResult)?.StatusCode);
        }

        [Fact]
        public async Task Register_ReturnsValidationProblem_WhenCreateUserFails()
        {
            // Arrange
            var identityErrors = new List<IdentityError>
            {
                new() { Code = "DuplicateUserName", Description = "Email already taken." },
                new() { Code = "PasswordTooShort", Description = "Password must be at least 6 characters." }
            };
            var failedResult = IdentityResult.Failed(identityErrors.ToArray());

            _userManagerMock
                .Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(failedResult);

            var signInManagerMock = GetMockSignInManager(_userManagerMock.Object);

            var controller = new AccountController(signInManagerMock.Object);

            var registerDto = new RegisterDto
            {
                Email = "existing@example.com",
                Password = "123"
            };

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Null(objectResult.StatusCode);

            // You can optionally verify the ModelState
            Assert.False(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.ContainsKey("DuplicateUserName"));
            Assert.True(controller.ModelState.ContainsKey("PasswordTooShort"));
        }

        [Fact]
        public async Task GetUserInfo_ReturnsNoContent_WhenUserNotAuthenticated()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity()) // not authenticated
                }
            };

            // Act
            var result = await _controller.GetUserInfo();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetUserInfo_ReturnsUnauthorized_WhenUserIsNull()
        {
            // Arrange
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                           .ReturnsAsync((User)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "test@example.com")
                }, "mock")) // authenticated
                }
            };

            // Act
            var result = await _controller.GetUserInfo();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetUserInfo_ReturnsOk_WithUserInfoAndRoles()
        {
            // Arrange
            var user = new User
            {
                Email = "test@example.com",
                UserName = "testuser"
            };

            var roles = new List<string> { "Admin", "Member" };

            var userManagerMock = GetMockUserManager();
            userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                           .ReturnsAsync(user);
            userManagerMock.Setup(um => um.GetRolesAsync(user))
                           .ReturnsAsync(roles);

            var signInManagerMock = GetMockSignInManager(userManagerMock.Object);

            var controller = new AccountController(signInManagerMock.Object);


            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "test@example.com")
                }, "mock")) // authenticated
                }
            };

            // Act
            var result = await controller.GetUserInfo();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            if (okResult.Value is { } obj)
            {
                Assert.Equal(user.Email, obj.GetType().GetProperty("Email")?.GetValue(obj)?.ToString());
                Assert.Equal(user.UserName, obj.GetType().GetProperty("UserName")?.GetValue(obj)?.ToString());
            }
        }

        [Fact]
        public async Task Logout_CallsSignOutAsync_AndReturnsNoContent()
        {
            // Arrange
            var userManagerMock = GetMockUserManager();
            var signInManagerMock = GetMockSignInManager(userManagerMock.Object);

            signInManagerMock.Setup(s => s.SignOutAsync())
                             .Returns(Task.CompletedTask)
                             .Verifiable();

            var controller = new AccountController(signInManagerMock.Object);

            // Act
            var result = await controller.Logout();

            // Assert
            signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }
        private static Mock<UserManager<User>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private static Mock<SignInManager<User>> GetMockSignInManager(UserManager<User> userManager)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            return new Mock<SignInManager<User>>(userManager,
                contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null);
        }

    }
}
