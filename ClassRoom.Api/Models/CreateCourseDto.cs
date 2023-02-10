using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models
{
    public class CreateCourseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
