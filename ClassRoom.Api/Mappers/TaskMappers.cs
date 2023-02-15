using ClassRoom.Api.Models;

namespace ClassRoom.Api.Mappers;

public static class TaskMappers
{
    public static void SetValue(this Entities.Task task, UpdateTaskDto updateTaskDto)
    {
        task.Title = updateTaskDto.Title;
        task.Description = updateTaskDto.Description;
        task.Status = updateTaskDto.Status;
        task.MaxScore = updateTaskDto.MaxScore;
        task.StartDate = updateTaskDto.StartDate;
        task.EndDate = updateTaskDto.EndDate;
    }
}
