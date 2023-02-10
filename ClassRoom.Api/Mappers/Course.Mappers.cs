using ClassRoom.Api.Entities;
using ClassRoom.Api.Models;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;
namespace ClassRoom.Api.Mappers;

public static class Mappers
{
    public static CourseDto ToDto (this Course course)
    {
        return new CourseDto()
        {
            Id = course.Id,
            Name = course.Name,
            Key = course.Key,
            Users = course.Users?.Select(userCourse => userCourse.User?.Adapt<UserDto>()).ToList()
        };
    }
}
