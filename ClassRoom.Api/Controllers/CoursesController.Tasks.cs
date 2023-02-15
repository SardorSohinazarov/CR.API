using ClassRoom.Api.Entities;
using ClassRoom.Api.Mappers;
using ClassRoom.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClassRoom.Api.Controllers
{
    public partial class CoursesController
    {
        [HttpGet("{courseId}/tasks/{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid courseId, Guid taskId)
        {
            //todo user course azosi va admini ekanini tekshirih kerak

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.CourseId == courseId && t.Id == taskId);
            if (task is null) return NotFound();

            return Ok(task.Adapt<TaskDto>());
        }


        [HttpGet("{courseId}/tasks")]
        public async Task<IActionResult> GetTasks(Guid courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == courseId);
            if (course is null) return NotFound();

            var tasks = course.Tasks.Select(task => task.Adapt<TaskDto>()).ToList();
            return Ok(tasks ?? new List<TaskDto>());
        }


        [HttpPost("{courseId}/tasks")]
        public async Task<IActionResult> AddTask(Guid courseId, [FromBody] CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == courseId);
            if (course is null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (course.Users.Any(uc => uc.UserId == user.Id && uc.IsAdmin) == false)
                return BadRequest();

            var task = createTaskDto.Adapt<Entities.Task>();
            task.CreatedDate = DateTime.Now;
            task.CourseId = courseId;

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return Ok(task.Adapt<TaskDto>());
        }


        [HttpPut("{courseId}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid courseId, Guid taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            //todo user course azosi va admini ekanini tekshirih kerak
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.CourseId == courseId && t.Id == taskId);
            if (task is null) return NotFound();

            task.SetValue(updateTaskDto);
            await _context.SaveChangesAsync();

            return Ok(task.Adapt<TaskDto>());
        }
        [HttpDelete("{courseId}/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid courseId, Guid taskId)
        {
            //todo user course azosi va admini ekanini tekshirih kerak

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.CourseId == courseId && t.Id == taskId);
            if (task is null) return NotFound();

            _context.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
