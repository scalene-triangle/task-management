using Microsoft.EntityFrameworkCore;
using task_management.Data;
using task_management.DTOs.MyTasks;
using task_management.Models;

namespace task_management.Services
{
    public class MyTaskService : IMyTaskService
    {
        private readonly AppDbContext _context;

        public MyTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseMyTaskDto> CreateTask(CreateMyTaskDto dto)
        {
            var story = await _context.Story.FindAsync(dto.StoryId);
            if (story == null)
            {
                throw new Exception("Invalid StoryId: Story does not exist.");
            }

            Project? project = null;
            if (dto.ProjectId.HasValue)
            {
                project = await _context.Project.FindAsync(dto.ProjectId.Value);
            }

            var newTask = new MyTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Estimate = dto.Estimate,
                ProjectId = dto.ProjectId,
                Project = project,
                StoryId = dto.StoryId,
                Story = story
            };

            _context.Task.Add(newTask);
            await _context.SaveChangesAsync();

            return new ResponseMyTaskDto(newTask);
        }

        public async Task<ResponseMyTaskDto> UpdateTask(int taskId, UpdateMyTaskDto dto)
        {
            var task = await _context.Task
                .Include(t => t.Project)
                .Include(t => t.Story)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new Exception($"Task with ID {taskId} not found.");
            }

            if (dto.StoryId.HasValue)
            {
                var story = await _context.Story.FindAsync(dto.StoryId.Value);
                if (story == null)
                {
                    throw new Exception($"Story with ID {dto.StoryId} not found.");
                }
                task.StoryId = dto.StoryId.Value;
                task.Story = story;
            }

            if (dto.ProjectId.HasValue)
            {
                var project = await _context.Project.FindAsync(dto.ProjectId.Value);
                if (project == null)
                {
                    throw new Exception($"Project with ID {dto.ProjectId} not found.");
                }
                task.ProjectId = dto.ProjectId.Value;
                task.Project = project;
            }

            task.Title = dto.Title ?? task.Title;
            task.Description = dto.Description ?? task.Description;
            task.DueDate = dto.DueDate ?? task.DueDate;
            task.Estimate = dto.Estimate ?? task.Estimate;

            _context.Task.Update(task);
            await _context.SaveChangesAsync();

            return new ResponseMyTaskDto(task);
        }
    }
}
