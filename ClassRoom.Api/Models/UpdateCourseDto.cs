using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models
{
    public class UpdateCourseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
