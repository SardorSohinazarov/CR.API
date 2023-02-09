using Microsoft.AspNetCore.Identity;

namespace ClassRoom.Api.Entities;

public class User: IdentityUser<Guid>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}
