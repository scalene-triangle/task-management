using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task_management.Models
{
    public class Story : GenericRecord
    {
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Name { get; set; } = "";

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        [StringLength(7)]
        [Required]
        public string Color { get; set; } = "#1c83a5";

        [Required]
        public int Order { get; set; }

        [Required]
        public Process Process { get; set; } = Process.Todo;
        [JsonIgnore] public virtual List<Task> Tasks { get; set; }
    }

    public enum Process
    {
        Todo,
        InProgress,
        Done,
        OnHold,
    }
}
