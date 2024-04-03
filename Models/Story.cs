using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task_management.Models
{
    public class Story : GenericRecord
    {
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Name { get; set; } = "";

        //[RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        [StringLength(7)]
        [Required]
        public string Color { get; set; } = "#1c83a5";

        [Range(1, int.MaxValue, ErrorMessage = "Order must be at least 1.")]
        [Required]
        public int Order { get; set; }

        [JsonIgnore] public virtual List<MyTask> MyTasks { get; set; } = new List<MyTask>();
    }
}
