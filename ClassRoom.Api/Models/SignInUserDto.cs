using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models;

public class SignInUserDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}