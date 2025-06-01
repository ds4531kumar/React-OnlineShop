using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(SignInManager<User> signInManager) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(registerDto.Email);
                ArgumentException.ThrowIfNullOrEmpty(registerDto.Password);
                var user = new User
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email
                };

                var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return ValidationProblem();
                }

                await signInManager.UserManager.AddToRoleAsync(user, "Member");
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false) return NoContent();

            var user = await signInManager.UserManager.GetUserAsync(User);

            if (user == null) return Unauthorized();

            var roles = await signInManager.UserManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Email,
                user.UserName,
                Roles = roles
            });
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }
        [HttpPost("Address")]
        public async Task<ActionResult<Address>> CreateOrUpdateAddress(Address address)
        {
            var user = await signInManager.UserManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (user == null) return Unauthorized();

            user.Address = address;
            var result = await signInManager.UserManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Problem while updating address");

            return Ok(user.Address);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<Address>> GetSavedAddress()
        {
            var address = await signInManager.UserManager.Users
                .Where(x => x.UserName == User.Identity!.Name)
                .Select(x => x.Address)
                .FirstOrDefaultAsync();

            if (address == null) return NotFound("No address found for this user");

            return address;
        }
    }
}
