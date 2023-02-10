using ClassRoom.Api.Entities;

namespace ClassRoom.Api.Models
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Key { get; set; }

        public virtual List<UserDto>? Users { get; set; }
    }
}
