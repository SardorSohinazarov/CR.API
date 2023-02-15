using ClassRoom.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClassRoom.Api.Models;

public class UpdateTaskDto
{
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxScore { get; set; }
    public ETaskStatus Status { get; set; }
}
