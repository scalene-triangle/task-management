

using System.Text.Json.Serialization;
using task_management.Models;

namespace task_management.DTOs.MyTasks
{
    public class ResponseMyTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ResponseTaskProject? Project { get; set; }
        public ResponseTaskStory Story { get; set; }

        public ResponseMyTaskDto(MyTask task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Project = task.Project is not null
                ? new ResponseTaskProject
                {
                    Id = task.Project.Id,
                    Name = task.Project.Name
                }
                : null;
            Story = new ResponseTaskStory
                {
                    Id = task.Story.Id,
                    Name = task.Story.Name
                };
        }

        public class ResponseTaskProject
        {
            [JsonPropertyName("id")] public int Id { get; set; }

            [JsonPropertyName("name")] public string Name { get; set; }
        }

        public class ResponseTaskStory
        {
            [JsonPropertyName("id")] public int Id { get; set; }

            [JsonPropertyName("name")] public string Name { get; set; }
        }
    }
}
