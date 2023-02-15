using ClassRoom.Api.Entities;
using ClassRoom.Api.Mappers;
using ClassRoom.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            List<CourseDto> courseDto = user.Courses.Select(userCourses => userCourses.Course.ToDto()).ToList();
            return Ok(courseDto);
        }
    }
}
