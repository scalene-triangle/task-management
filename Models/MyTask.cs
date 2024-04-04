using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task_management.Models
{
    public class MyTask: GenericRecord
    {
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Estimate In Hours")]
        [Range(0, 24, ErrorMessage = "Value must be between 0 and 24.")]
        public decimal Estimate { get; set; } = 0;

        [Display(Name = "Project")]
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")] public virtual Project? Project { get; set; }

        [Display(Name = "Story")]
        [Required] public int StoryId { get; set; }
        [ForeignKey("StoryId")] public virtual Story Story { get; set; }
    }
}
