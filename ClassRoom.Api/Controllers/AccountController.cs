using ClassRoom.Api.Entities;
using ClassRoom.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        // IUserStore interfaceni implementatsiyasi
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpUserDto createUserDto)
        {
            if ( ! ModelState.IsValid || createUserDto.Password != createUserDto.ConfirmPassword )
            {
                return BadRequest();
            }
            // mapping <PackageReference Include="Mapster" Version="6.5.1" />
            var user = createUserDto.Adapt<User>();

            // userni database ga saqlab beradi
            await _userManager.CreateAsync(user, createUserDto.Password);

            // token yaratib cookiega yozib yuboradi
            await _signInManager.SignInAsync(user, isPersistent:true);

            return Ok();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInUserDto signInUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _userManager.Users.AnyAsync(user => user.UserName == signInUserDto.Username))
                return NotFound();

            var result = await _signInManager.PasswordSignInAsync(signInUserDto.Username, signInUserDto.Password, isPersistent: true, false);

            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }
    }
}
