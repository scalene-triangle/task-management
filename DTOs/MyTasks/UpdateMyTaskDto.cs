using System.Text.Json.Serialization;

namespace task_management.DTOs.MyTasks
{
    public class UpdateMyTaskDto
    {
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("description")] public string? Description { get; set; }
        [JsonPropertyName("dueDate")] public DateTime? DueDate { get; set; }
        [JsonPropertyName("estimate")] public decimal? Estimate { get; set; }
        [JsonPropertyName("projectId")] public int? ProjectId { get; set; }
        [JsonPropertyName("storyId")] public int? StoryId { get; set; }
    }
}
