using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models;

public class SignInUserDto
{
    [Required]
    public string? Username { get; set; }


    [Required]
    public string? Password { get; set; }
}