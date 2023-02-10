using ClassRoom.Api.Context;
using ClassRoom.Api.Entities;
using ClassRoom.Api.Mappers;
using ClassRoom.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly AppLicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CoursesController(
            AppLicationDbContext context,
            UserManager<User> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            List<CourseDto> listofCourseDto = courses.Select(c => c.ToDto()).ToList();
            return Ok(listofCourseDto);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if(course == null)
                return NotFound();

            return Ok(course.ToDto());
        }
            
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.GetUserAsync(User);

            Course course = new Course()
            {
                Name = createCourseDto.Name,
                Key = Guid.NewGuid().ToString("N"),
                Users = new List<UserCourse>()
                {
                    new UserCourse()
                    {
                        UserId = user.Id,
                        IsAdmin = true
                    }
                }
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);

            return Ok(course?.ToDto());
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateCourse(Guid id,[FromBody] UpdateCourseDto updateCourseDto)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();



            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course is null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (course.Users?.Any(uc => uc.UserId == user.Id) != true)
                return BadRequest();

            course.Name = updateCourseDto.Name;
            await _context.SaveChangesAsync();

            return Ok(course.ToDto());
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (course.Users?.Any(uc => uc.UserId == user.Id) != true)
                return BadRequest();
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
