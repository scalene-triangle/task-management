using task_management.DTOs.MyTasks;
using task_management.Models;

namespace task_management.Services
{
    public interface IMyTaskService
    {
        Task<ResponseMyTaskDto> CreateTask(CreateMyTaskDto dto);
    }
}