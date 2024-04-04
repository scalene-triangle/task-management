using Humanizer;
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

            var responseDto = new ResponseMyTaskDto(newTask);
            return responseDto;
        }
    }
}
