using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
